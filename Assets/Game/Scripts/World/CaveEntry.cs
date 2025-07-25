using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveEntry : MonoBehaviour
{
    [SerializeField] private string nextScene;

    void OnTriggerEnter2D()
    {
        GameManager.Instance.playerPosition = new Vector3 (0, 0, 0);
        SceneManager.LoadScene(nextScene);
    }
}
