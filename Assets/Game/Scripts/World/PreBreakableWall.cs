using UnityEngine;

public class PreBreakableWall : MonoBehaviour
{
    public string[] falas;
    private PlayerMovement playerScript;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.preBreakableWall)
        {
            playerScript = other.GetComponent<PlayerMovement>();
            GameManager.Instance.preBreakableWall = true;
            playerScript.canMove = false;
            DialogoManager.Instance.IniciarDialogo(falas, CanMove);
        }
    }

    void CanMove()
    {
        playerScript.canMove = true;
    }
}
