using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private GameObject defaultSelected; // default button selected when game starts
    [SerializeField] private string playingScene; //might change its name later
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;

    public void Start()
    {

        EventSystem.current.SetSelectedGameObject(defaultSelected); // selects the default button

        //This automatize it, but it can also be done by just setting it off when making the game build
        if(settingsMenuPanel != null)
        {
            settingsMenuPanel.SetActive(false);
        }
    } 

    public void Play()
    {
        SceneManager.LoadScene(playingScene);
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }
}
