using UnityEngine;
using UnityEngine.SceneManagement;

public class OrcAreaExit : MonoBehaviour
{
    [SerializeField] private string nextScene;

    void OnTriggerEnter2D()
    {
        GameManager.Instance.playerPosition = new Vector3 (7f, 30f, 0f);
        SceneManager.LoadScene(nextScene);
    }
}
