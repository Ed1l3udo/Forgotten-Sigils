using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 5f; 
    public bool canMove = true;
    private Camera mainCamera;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 playerMovementDirection; 
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 24f;
    private float dashTime = 0.2f;
    public float dashCooldown = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

    }

    void Update()
    {
        
        if(isDashing)
        {
            return;
        }

        MovePlayer();
        AdjustSprite();
        KeyHandler();
        
    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        rb.linearVelocity = playerMovementDirection.normalized * playerMoveSpeed;
    }


    void MovePlayer()
    {
        // Entrada do jogador (WASD ou setas)
        playerMovementDirection.x = Input.GetAxisRaw("Horizontal"); // Pega a entrada A/D e retorna 1(D) -1(A) ou 0 (nada) 
        playerMovementDirection.y = Input.GetAxisRaw("Vertical"); // Pega a entrada W/S e retorna 1 (W) -1 (S) ou 0 (nada)
    }

    void AdjustSprite()
    {
        // Conditional to invert sprite
        animator.SetBool("isWalking", playerMovementDirection != Vector2.zero);
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mouseWorldPos - transform.position;
        animator.SetBool("lookingUp", directionToMouse.y > 0);

        Vector3 scale = transform.localScale;
        if (directionToMouse.x >= 0) scale.x = Mathf.Abs(scale.x);
        else if (directionToMouse.x < 0) scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void KeyHandler()
    {
        // Dash
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("Dashing");
        canDash = false;
        isDashing = true;
        rb.linearVelocity = playerMovementDirection.normalized * dashPower;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
