using UnityEngine;
using UnityEngine.SceneManagement;

public class MeleeCaveEntry : MonoBehaviour
{
    [SerializeField] private string nextScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.playerPosition = new Vector3 (-45.5f, 4.5f, 0f);
            SceneManager.LoadScene(nextScene);
        }
    }
}
