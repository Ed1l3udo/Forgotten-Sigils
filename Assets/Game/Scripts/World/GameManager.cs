using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Dados do jogador
    public int playerMaxHealth = 4;
    public int playerCurrentHealth;
    public int playerMaxMana = 27;
    public int playerCurrentMana;
    [SerializeField] public Vector3 playerPosition;
    public GameObject fireBallPrefab;
    public GameObject windPrefab;
    public GameObject forceBallPrefab;
    public GameObject meelePrefab;
    public List<BaseMagic> availableMagics;
    public bool grassHasBeenBroken = false;
    public bool caveWallHasBeenBroken = false;
    public bool runaAppeared = false;
    public bool fireAvailable = false;
    public bool windAvailable = false;
    public bool forceAvailable = false;
    public bool dashAvailable = false;
    public bool meleeAvailable = false;
    public bool healAvailable = false;
    public bool hasNightVision = false;
    public bool rockMoved = false;
    public bool deadOrc = false;

    void Awake()
    {
        playerPosition = new Vector3(-120, -135, 0); // inicio jogo
        // playerPosition = new Vector3(0, 0, 0);
        // playerPosition = new Vector3(-1f, -38f, 0); //inicio caverna
        // playerPosition = new Vector3(-20f, -60f, 0); //runa vento
        // playerPosition = new Vector3(-200f, -35f, 0); // inicio orcs


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
