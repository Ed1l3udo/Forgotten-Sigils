using UnityEngine;
using System.Collections;

public class LesserOrc : Damageable
{
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private GameObject playerGameObject;
    private int currentHealth;
    private Transform player;
    private bool aggro;
    private bool isAttacking = false; // <- NOVO
    private Rigidbody2D rb;
    private Animator animator;
    private float distanceToPlayer;
    public float offsetDistance = 0.5f;
    private Vector2 direction;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (playerGameObject != null) player = playerGameObject.transform;
        else
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            else Debug.Log("Não foi possível achar o player!");
        }
    }

    void Update()
    {
        if (player == null) return;

        direction = (player.position - transform.position).normalized;
        AdjustSprite(direction);
        distanceToPlayer = (player.position - transform.position).magnitude;

        if (!aggro) {
            aggro = distanceToPlayer < 10f;
            animator.SetBool("Aggro", aggro);
        }

        if (aggro && distanceToPlayer < 2f && !isAttacking)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(PerformAttack()); 
        }
    }

    void FixedUpdate()
    {
        if (aggro && !isAttacking)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    IEnumerator PerformAttack()
    {
        if (isAttacking) yield break;

        isAttacking = true;

        // Vector3 spawnPosition = transform.position + (Vector3)(direction.normalized * offsetDistance);

        // GameObject slash = Instantiate(slashPrefab, spawnPosition, Quaternion.identity);
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // slash.transform.rotation = Quaternion.Euler(0, 0, angle);

        yield return new WaitForSeconds(0.833f);

        isAttacking = false;
    }


    public override void TakeDamage(int dano)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= dano;

        ShowDamageEffect();

        if (currentHealth <= 0) Die();
    }
}
