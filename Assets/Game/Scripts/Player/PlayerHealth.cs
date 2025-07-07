using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthUI HealthPanel;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        HealthPanel.UpdateUI(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // doesn't let currentHealth go beyond 0 or maxHealth
        HealthPanel.UpdateUI(currentHealth);

        if(currentHealth == 0) Die();
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // doesn't let currentHealth go beyond 0 or maxHealth
        HealthPanel.UpdateUI(currentHealth);
    }

    public void Die()
    {
        // a ser implementado
    }
}
