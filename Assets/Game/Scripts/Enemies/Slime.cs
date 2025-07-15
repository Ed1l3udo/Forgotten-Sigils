using UnityEngine;

public class Slime : Damageable
{
    public int maxHealth = 20;
    private int currentHealth;

    void Start()
    {
        base.Start();
        currentHealth = maxHealth;
    }

    public override void TakeDamage(int dano)
    {
        currentHealth -= dano;
        Debug.Log("Slime levou " + dano + " de dano. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
