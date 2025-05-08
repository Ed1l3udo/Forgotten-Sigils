using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 movement;
    public bool canMove = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 24f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;
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

        // Entrada do jogador (WASD ou setas)
        movement.x = Input.GetAxisRaw("Horizontal"); // Pega a entrada A/D e retorna 1(D) -1(A) ou 0 (nada) 
        movement.y = Input.GetAxisRaw("Vertical"); // Pega a entrada W/S e retorna 1 (W) -1 (S) ou 0 (nada)

        animator.SetBool("isWalking", movement != Vector2.zero);

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mouseWorldPos - transform.position;
        animator.SetBool("lookingUp", directionToMouse.y > 0);

        // Condicional pra inverter o sprite
        Vector3 scale = transform.localScale;
        if (directionToMouse.x >= 0) scale.x = Mathf.Abs(scale.x);
        else if (directionToMouse.x < 0) scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

    private IEnumerator Dash()
    {
        Debug.Log("Dashing");
        canDash = false;
        isDashing = true;
        rb.linearVelocity = movement.normalized * dashPower;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
