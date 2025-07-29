using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthUI HealthPanel;
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject kingOrc;
    private int currentHealth;
    public bool foiAtacadoDuranteCura = false;

    void Start(){
        currentHealth = GameManager.Instance.playerCurrentHealth;
        maxHealth = GameManager.Instance.playerMaxHealth;

        HealthPanel = FindObjectOfType<HealthUI>();

        if (HealthPanel != null) HealthPanel.UpdateUI(currentHealth);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // doesn't let currentHealth go beyond 0 or maxHealth
        HealthPanel.UpdateUI(currentHealth);

        GameManager.Instance.playerCurrentHealth = currentHealth;
        
        foiAtacadoDuranteCura = true;

        if (currentHealth == 0) Die();
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // doesn't let currentHealth go beyond 0 or maxHealth
        HealthPanel.UpdateUI(currentHealth);

        GameManager.Instance.playerCurrentHealth = currentHealth;
    }

    public void Die()
    {
        GameManager.Instance.playerPosition = GameManager.Instance.checkpoint;
        if(SceneManager.GetActiveScene().name != GameManager.Instance.checkpointScene) SceneManager.LoadScene(GameManager.Instance.checkpointScene);
        currentHealth = maxHealth;
        HealthPanel.UpdateUI(currentHealth);
    }
}
