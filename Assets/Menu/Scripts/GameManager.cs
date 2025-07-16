using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Dados do jogador
    public int playerMaxHealth = 5;
    public int playerCurrentHealth;
    public int playerMaxMana = 27;
    public int playerCurrentMana;
    public Vector3 playerPosition;

    void Awake()
    {
        // Implementação do Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerCurrentHealth = playerMaxHealth; // Inicializa
            playerCurrentMana = playerMaxMana;
        }
        else
        {
            Destroy(gameObject); // Evita duplicação
        }
    }
}
