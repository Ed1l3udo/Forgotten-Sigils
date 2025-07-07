using UnityEngine;

public class Slime : MonoBehaviour, IDamageable
{
    public int maxHealth = 20;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dano)
    {
        currentHealth -= dano;
        Debug.Log("Slime levou " + dano + " de dano. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Slime morreu");
        Destroy(gameObject);
    }
}
