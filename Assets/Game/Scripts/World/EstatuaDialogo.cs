using UnityEngine;

public class EstatuaDialogo : MonoBehaviour
{
    
    public GameObject fireRune;            
    public string[] falas;
    public GameObject interactionMessage;
    private bool playerNearby = false;
    private GameObject playerRef;
    private bool ativado = false;


    void Update()
    {
        if (playerNearby && !ativado && Input.GetKeyDown(KeyCode.R))
        {
            ativado = true;
            interactionMessage.SetActive(false);
            if (GameManager.Instance.runaAppeared)
            {
                DialogoManager.Instance.IniciarDialogo(falas);
            }
            else
            {
                GameManager.Instance.runaAppeared = true;
                DialogoManager.Instance.IniciarDialogo(falas, SpawnRunaDeFogo);
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

    void SpawnRunaDeFogo()
    {
        fireRune.SetActive(true);
        Debug.Log("Runa de fogo criada pela estátua!");
        interactionMessage.SetActive(true);
    }

}
