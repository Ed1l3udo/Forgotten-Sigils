using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Dados do jogador
    public int playerMaxHealth = 5;
    public int playerCurrentHealth;
    public int playerMaxMana = 27;
    public int playerCurrentMana;
    [SerializeField] public Vector3 playerPosition;
    public GameObject fireBallPrefab;
    public GameObject windPrefab;
    public GameObject forceBallPrefab;

    public List<BaseMagic> availableMagics;
    public bool fireAvailable;
    public bool windAvailable;
    public bool forceAvailable;
    public bool dashAvailable;

    void Awake()
    {
        playerPosition = new Vector3(0f, 0f, 0f);

        if (fireAvailable) availableMagics.Add(new FireBall(fireBallPrefab, 1));
        if (windAvailable) availableMagics.Add(new WindBlast(windPrefab, 1));
        if (forceAvailable) availableMagics.Add(new Force(forceBallPrefab, 1));

        // Implementação do Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerCurrentHealth = playerMaxHealth; // Inicializa
            playerCurrentMana = playerMaxMana;
            availableMagics = new List<BaseMagic>();
        }
        else
        {
            Destroy(gameObject); // Evita duplicação
        }
    }
}
