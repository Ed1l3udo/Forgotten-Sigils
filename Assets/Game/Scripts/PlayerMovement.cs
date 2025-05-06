using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 movement;
    public bool canMove = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    
        // Entrada do jogador (WASD ou setas)
        movement.x = Input.GetAxisRaw("Horizontal"); // Pega a entrada A/D e retorna 1(D) -1(A) ou 0 (nada) 
        movement.y = Input.GetAxisRaw("Vertical"); // Pega a entrada W/S e retorna 1 (W) -1 (S) ou 0 (nada)

        animator.SetBool("isWalking", movement != Vector2.zero);
        animator.SetBool("walkingUpwards", movement.y > 0);
        animator.SetBool("walkingDownwards", movement.y < 0);

        // Condicional pra inverter o sprite
        Vector3 scale = transform.localScale;
        if (movement.x > 0) scale.x = Mathf.Abs(scale.x);
        else if (movement.x < 0) scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

}
