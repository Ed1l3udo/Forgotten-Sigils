using UnityEngine;
using System.Collections;

public class Log : MonoBehaviour, IDamageable
{
    public GameObject playerGameObject;
    private Transform player;
    public int maxHealth = 20;
    private int currentHealth;
    public float detectionRange = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float pauseDuration = 1f;
    public int damage = 1;

    private bool isAwake = false;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;

        if (playerGameObject != null)
        {
            player = playerGameObject.transform;
        }
        else
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
            else Debug.Log("Não foi possível achar o player!");
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if(!isAwake) animator.SetBool("isAwake", false);

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null) player = p.transform;
        }

        StartCoroutine(BehaviorRoutine());
    }

    void Update()
    {
        if (isAwake && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            UpdateVisualDirection(direction);
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

    public void TakeDamage(int damage){
        currentHealth -= damage;
        
        if(currentHealth <= 0) Die();
    }

    void Die(){
        Destroy(gameObject);
    }

    IEnumerator BehaviorRoutine()
    {
        while (true)
        {
            if (!isAwake)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer <= detectionRange)
                {
                    isAwake = true;
                    animator.SetBool("isAwake", true);
                }
            }

            if (isAwake)
            {
                yield return StartCoroutine(DashRoutine());
            }
            else
            {
                yield return null; // espera o próximo frame
            }
        }
    }

    IEnumerator DashRoutine()
    {
        while (true)
        {
            if (player == null) yield break;

            // Calcular direção até o player
            Vector2 direction = (player.position - transform.position).normalized;

            // Iniciar dash
            rb.linearVelocity = direction * dashSpeed;

            // Espera o tempo do dash
            yield return new WaitForSeconds(dashDuration);

            // Para o movimento
            rb.linearVelocity = Vector2.zero;

            // Espera a pausa antes do próximo dash
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    void UpdateVisualDirection(Vector2 direction)
    {
        if (animator != null)
        {
            animator.SetBool("lookingUp", direction.y > 0);
        }

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (direction.x >= 0 ? 1 : -1);
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
