using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCaveExit : MonoBehaviour
{
    [SerializeField] private string nextScene;
    public GameObject interactionMessage;
    private bool playerNearby = false;
    private GameObject playerRef;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.playerPosition = new Vector3 (28f, 24f, 0f);
            SceneManager.LoadScene(nextScene);
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
