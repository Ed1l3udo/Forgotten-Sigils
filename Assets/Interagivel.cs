using UnityEngine;

public class Interagivel : MonoBehaviour
{
    public string[] falas;
    private bool podeInteragir = false;
    private DialogoManager dialogoManager;
    public GameObject interactionMessage;
    private bool playerNearby = false;
    private GameObject playerRef;

    void Start()
    {
        dialogoManager = FindObjectOfType<DialogoManager>();
    }

    void Update()
    {
        if (podeInteragir && Input.GetKeyDown(KeyCode.R)) // tecla de interação
        {
            dialogoManager.IniciarDialogo(falas);
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
}
