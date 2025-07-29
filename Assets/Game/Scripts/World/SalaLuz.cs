using UnityEngine;

public class SalaLuz : MonoBehaviour
{
    public string[] falas;
    private PlayerMovement playerScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.salaLuz)
        {
            playerScript = other.GetComponent<PlayerMovement>();
            GameManager.Instance.salaLuz = true;
            playerScript.canMove = false;
            DialogoManager.Instance.IniciarDialogo(falas, CanMove);
        }
    }

    void CanMove()
    {
        playerScript.canMove = true;
    }
}
