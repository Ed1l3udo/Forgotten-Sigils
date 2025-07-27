using UnityEngine;
using UnityEngine.SceneManagement;

public class MeleeCaveExit : MonoBehaviour
{
    [SerializeField] private string nextScene;

    void OnTriggerEnter2D()
    {
        GameManager.Instance.playerPosition = new Vector3 (-123f, -91f, 0f);
        SceneManager.LoadScene(nextScene);
    }
}
