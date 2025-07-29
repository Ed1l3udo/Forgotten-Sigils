using UnityEngine;

public class TriggerDeDialogo : MonoBehaviour
{
    public string[] falas;
    private PlayerMovement playerScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.preHealRune)
        {
            playerScript = other.GetComponent<PlayerMovement>();
            GameManager.Instance.preHealRune = true;
            playerScript.canMove = false;
            DialogoManager.Instance.IniciarDialogo(falas, CanMove);
        }
    }

    void CanMove()
    {
        playerScript.canMove = true;
    }
}
