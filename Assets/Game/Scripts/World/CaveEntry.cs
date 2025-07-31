using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveEntry : MonoBehaviour
{
    [SerializeField] private string nextScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.playerPosition = new Vector3 (-1.5f, -59f, 0);
            GameManager.Instance.checkpoint = new Vector3 (-1.5f, -59f, 0);
            GameManager.Instance.checkpointScene = "Cave";
            SceneManager.LoadScene(nextScene);
        }
    }
}
