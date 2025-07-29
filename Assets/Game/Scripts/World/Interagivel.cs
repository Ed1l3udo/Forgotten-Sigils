using UnityEngine;

public class Interagivel : MonoBehaviour
{
    public string[] falas;
    public GameObject interactionMessage;
    private bool playerNearby = false;
    private GameObject playerRef;
    public bool ativado = false;

    void Update()
    {
        if (playerNearby && !ativado && Input.GetKeyDown(KeyCode.R)) // tecla de interação
        {
            DialogoManager.Instance.IniciarDialogo(falas, VoltarR);
            interactionMessage.SetActive(false);
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

    void VoltarR()
    {
        interactionMessage.SetActive(true);
    }
}
