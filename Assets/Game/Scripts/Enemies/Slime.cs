using UnityEngine;

public class Slime : Damageable
{
    public int maxHealth = 20;
    private int currentHealth;
    public float moveSpeed = 2f;
    private Transform player;

    void Start()
    {
        base.Start();
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player não encontrado! Certifique-se de que o jogador tenha a tag 'Player'.");
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public override void TakeDamage(int dano)
    {
        currentHealth -= dano;

        ShowDamageEffect();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
