using UnityEngine;

public class SemMana : MonoBehaviour
{
    private PlayerMana playerScript;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript = other.GetComponent<PlayerMana>();
            GameManager.Instance.travarMana = true;
            playerScript.currentMana = 0;
            playerScript.manaPanel.UpdateUI(0); 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.travarMana = false;
        }
    }
}