using UnityEngine;
using System.Collections;

public class KingOrc : Damageable
{
    [SerializeField] private int maxHealth = 200;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage = 20;
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private float minionSpawnCooldown = 10f;
    [SerializeField] private float dashCooldown = 8f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashDelay = 0.5f; // tempo antes de iniciar o dash real

    private int currentHealth;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private float minionSpawnTimer;
    private float dashTimer;
    private bool isDashing = false;
    private float dashTimeLeft;
    private bool isPreparingDash = false;
    private Vector2 direction;
    private Rigidbody2D rb;
    [SerializeField] private bool aggro = true;

    private void Start()
    {
        currentHealth = maxHealth;
        player = playerGameObject.transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        minionSpawnTimer = 0f;
        dashTimer = 0f;
        if (aggro) animator.SetBool("Aggro", true);
    }

    private void Update()
    {
        direction = player.position - transform.position;
        AdjustSprite(direction);

        if (isDashing)
        {
            DashMovement();
            return;
        }

        minionSpawnTimer += Time.deltaTime;
        if (minionSpawnTimer >= minionSpawnCooldown)
        {
            float chance = Random.Range(0f, 1f);
            if (!isDashing && !isAttacking && chance < 0.4f) StartCoroutine(InvocarLacaio());
            minionSpawnTimer = 0f;
        }

        dashTimer += Time.deltaTime;
        if (!isAttacking && dashTimer >= dashCooldown && !isPreparingDash && !isDashing)
        {
            float chance = Random.Range(0f, 1f);
            if (chance < 0.5f) PrepararDash();
            dashTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking && !isPreparingDash && aggro)
        {
            rb.linearVelocity = direction.normalized * speed;

            if (direction.magnitude < 1.5f)
            {
                StartCoroutine(PerformAttack());
                animator.SetTrigger("Attack");
            }
        }
        else rb.linearVelocity = Vector2.zero;
    }

    IEnumerator InvocarLacaio()
    {
        if (isAttacking) yield break;

        isAttacking = true;

        Debug.Log("Summoning!");
        int quantidade = Random.Range(5, 10);

        for (int i = 0; i < quantidade; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 5f;
            Vector3 spawnPosition = transform.position + (Vector3)offset;

            Instantiate(minionPrefab, spawnPosition, Quaternion.identity);
        }

        animator.SetTrigger("Summon");

        yield return new WaitForSeconds(0.850f);

        isAttacking = false;
    }

    IEnumerator PerformAttack()
    {
        if (isAttacking) yield break;

        isAttacking = true;

        // Vector3 spawnPosition = transform.position + (Vector3)(direction.normalized * offsetDistance);

        // GameObject slash = Instantiate(slashPrefab, spawnPosition, Quaternion.identity);
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // slash.transform.rotation = Quaternion.Euler(0, 0, angle);

        yield return new WaitForSeconds(0.667f);

        isAttacking = false;
    }

    private void PrepararDash()
    {
        isPreparingDash = true;
        animator.SetBool("Dash", true); // Ativa a animação de preparação
        Invoke(nameof(RealizarDash), dashDelay); // Aguarda antes de iniciar o dash real
    }

    private void RealizarDash()
    {
        Debug.Log("Dashing!");
        isPreparingDash = false;
        isDashing = true;
        dashTimeLeft = dashDuration;
    }

    private void DashMovement()
    {
        if (dashTimeLeft > 0f)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * dashSpeed * Time.deltaTime;
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            animator.SetBool("Dash", false); // Finaliza o estado de dash
        }
    }

    void AdjustSprite(Vector2 direction)
    {
        if (direction.magnitude < 0.01f) return;

        animator.SetBool("lookingUp", false);
        animator.SetBool("lookingDown", false);
        animator.SetBool("lookingLeft", false);
        animator.SetBool("lookingRight", false);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0) animator.SetBool("lookingRight", true);
            else animator.SetBool("lookingLeft", true);
        }
        else
        {
            if (direction.y > 0) animator.SetBool("lookingUp", true);
            else animator.SetBool("lookingDown", true);
        }
    }

    public override void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // public void StartFight()
    // {
    //     aggro = true;
    //     animator.SetBool("Aggro", true);
    // }

    private void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f); // Tempo para a animação
    }
}
