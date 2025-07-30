using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToOrcArea : MonoBehaviour
{
    [SerializeField] private string nextScene;

    void OnTriggerEnter2D()
    {
        GameManager.Instance.playerPosition = new Vector3 (-193f, -38f, 0f);
        SceneManager.LoadScene(nextScene);
    }
}
