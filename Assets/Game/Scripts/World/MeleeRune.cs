using UnityEngine;

public class MeleeRune : MonoBehaviour
{
    public GameObject particleEffect;
    private GameObject efeitoAtivo;
    public RunesUI runesUI;
    public GameObject interactionMessage;
    private bool playerNearby = false;
    private GameObject playerRef;
    public int meleeManaCost = 0;
    public float floatHeight = 0.5f;
    public float floatSpeed = 2f;
    private Vector3 startPosition;
    private bool collided = false;

    void Start()
    {
        if (GameManager.Instance.forceAvailable) Destroy(gameObject);
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        float scale = 1 + Mathf.Sin(Time.time * floatSpeed) * 0.1f;
        transform.localScale = new Vector3(scale, scale, scale);

        if (playerNearby && Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.Instance.hasNightVision)
            {
                ActivateRune();
            }
            else
            {
                // fazer caixa de texto
            }
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
        GameManager.Instance.meleeAvailable = true;
        Destroy(gameObject, 4f);
        if (particleEffect != null) efeitoAtivo = Instantiate(particleEffect, transform.position, Quaternion.identity);
        if (interactionMessage != null) interactionMessage.SetActive(false);
        runesUI.UpdateRunes();
    }
}
