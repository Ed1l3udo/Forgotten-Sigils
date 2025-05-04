using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed = 5f;

    private Vector2 movement;
    // Vetor que armazena o movimento

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Entrada do jogador (WASD ou setas)
        movement.x = Input.GetAxisRaw("Horizontal"); // Pega a entrada A/D e retorna 1(D) -1(A) ou 0 (nada) 
        movement.y = Input.GetAxisRaw("Vertical"); // Pega a entrada W/S e retorna 1 (W) -1 (S) ou 0 (nada)

        if(movement.x != 0 || movement.y != 0) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);

        if(movement.y > 0) animator.SetBool("walkingUpwards", true);
        else if(movement.y < 0) animator.SetBool("walkingUpwards", false);

        if(movement.y < 0) animator.SetBool("walkingDownwards", true);
        else if(movement.y > 0) animator.SetBool("walkingDownwards", false);

        transform.Translate(movement.normalized * moveSpeed * Time.deltaTime);

        // Condicional pra inverter o sprite
        Vector3 scale = transform.localScale;

        if (movement.x > 0) scale.x = Mathf.Abs(scale.x);
        else if (movement.x < 0) scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    /*
    void FixedUpdate()
    {
        // Move o objeto na direção do vetor movimento
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }
    */
}
