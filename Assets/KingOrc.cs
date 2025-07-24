using UnityEngine;

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

    private void Start()
    {
        currentHealth = maxHealth;
        player = playerGameObject.transform;
        animator = GetComponent<Animator>();
        minionSpawnTimer = 0f;
        dashTimer = 0f;
    }

    private void Update()
    {
        direction = (player.position - transform.position).normalized;

        if (isDashing)
        {
            DashMovement();
            return;
        }

        if (!isAttacking && !isPreparingDash)
        {
            MoveTowardPlayer();
        }

        minionSpawnTimer += Time.deltaTime;
        if (minionSpawnTimer >= minionSpawnCooldown)
        {
            float chance = Random.Range(0f, 1f);
            if (chance < 0.4f) InvocarLacaio();
            minionSpawnTimer = 0f;
        }

        dashTimer += Time.deltaTime;
        if (dashTimer >= dashCooldown && !isPreparingDash && !isDashing)
        {
            float chance = Random.Range(0f, 1f);
            if (chance < 0.5f) PrepararDash();
            dashTimer = 0f;
        }
    }

    private void MoveTowardPlayer()
    {
        Vector2 direction = player.position - transform.position;
        
        if (direction.magnitude > 1.5f) transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;

        else Attack();
        
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    private void InvocarLacaio()
    {
        Debug.Log("Summoning!");
        int quantidade = Random.Range(5, 10);

        for (int i = 0; i < quantidade; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 5f;
            Vector3 spawnPosition = transform.position + (Vector3)offset;

            Instantiate(minionPrefab, spawnPosition, Quaternion.identity);
        }

        animator.SetTrigger("Summon");
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

    private void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f); // Tempo para a animação
    }
}
