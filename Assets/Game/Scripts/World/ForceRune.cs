using UnityEngine;

public class ForceRune : MonoBehaviour
{
    public GameObject particleEffect;
    private GameObject efeitoAtivo;
    public GameObject forceBallPrefab;
    public RunesUI runesUI;
    public GameObject interactionMessage;
    private bool playerNearby = false;
    private GameObject playerRef;
    public string[] falas;
    private DialogoManager dialogoManager;
    public int forceManaCost = 9;
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;
    private Vector3 startPosition;
    private bool ativado = false;

    void Start()
    {
        if (GameManager.Instance.forceAvailable)
        {
            Destroy(gameObject);
            return;
        }
        startPosition = transform.position;
    }

    void Update()
    {
        if (ativado) return;
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        float scale = 1 + Mathf.Sin(Time.time * floatSpeed) * 0.1f;
        transform.localScale = new Vector3(scale, scale, scale);

        if (playerNearby && Input.GetKeyDown(KeyCode.R))
        {
            if (interactionMessage != null)
                interactionMessage.SetActive(false);
            DialogoManager.Instance.IniciarDialogo(falas, ActivateRune);
            ativado = true; // impede reativar
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            playerRef = other.gameObject;

            // Exibir mensagem de interação
            if (interactionMessage != null)
            {
                interactionMessage.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            playerRef = null;

            if (interactionMessage != null)
            {
                interactionMessage.SetActive(false);
            }
        }
    }


    void ActivateRune()
    {
        GameManager.Instance.forceAvailable = true;

        GameManager.Instance.availableMagics.Add(new Force(forceBallPrefab, forceManaCost));

        PlayerMagics playerMagics = playerRef.GetComponent<PlayerMagics>();
        playerMagics.AtualizarMagias();

        Destroy(gameObject, 4f);
        if (particleEffect != null) efeitoAtivo = Instantiate(particleEffect, transform.position, Quaternion.identity);
        if (interactionMessage != null) interactionMessage.SetActive(false);
        runesUI.UpdateRunes();
    }
}
