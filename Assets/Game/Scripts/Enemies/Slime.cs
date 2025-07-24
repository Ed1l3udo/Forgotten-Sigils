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

        ShowDamageEffect();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
