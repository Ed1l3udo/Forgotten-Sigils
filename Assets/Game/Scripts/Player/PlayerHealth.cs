using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthUI HealthPanel;
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject kingOrc;
    // public GameObject menuMorte;
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

        if (currentHealth < 1) Die();
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
        // if (menuMorte != null)
        // {
        //     menuMorte.GetComponent<GameOverMenu>().AtivarMenuDeMorte();
        // }
        // else
        // {
        //     Debug.LogWarning("Menu de Morte não atribuído!");
        // }
        GameManager.Instance.Resetar();
        GameManager.Instance.playerPosition = new Vector3(-120, -135, 0);
        SceneManager.LoadScene("Menu");
    }   
}
