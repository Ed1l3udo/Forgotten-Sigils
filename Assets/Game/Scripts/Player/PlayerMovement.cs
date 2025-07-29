using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 5f; 
    public bool canMove = true;
    private Camera mainCamera;
    private Animator animator;
    private TrailRenderer trailRenderer;
    public RunesUI runesUI;
    private Rigidbody2D rb;
    private Vector2 playerMovementDirection; 
    private bool canDash = true;
    private bool isDashing;
    private float dashPower = 24f;
    private float dashTime = 0.2f;
    public float dashCooldown = 3f;
    
    private float stepTimer = 0f;
    private float stepDelay = 0.4f; 
    private bool pisouEsquerda = true;

    void Start()
    {
        mainCamera = Camera.main;
        transform.position = GameManager.Instance.playerPosition;
        mainCamera.transform.position = new Vector3(GameManager.Instance.playerPosition.x, GameManager.Instance.playerPosition.y, -10f);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;

    }

    void Update()
    {

        if (isDashing)
        {
            return;
        }

        if (canMove)
        {
            MovePlayer();
            KeyHandler();
        }
        else playerMovementDirection = Vector2.zero;
        
        AdjustSprite();
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
        // Entrada do jogador
        playerMovementDirection.x = Input.GetAxisRaw("Horizontal");
        playerMovementDirection.y = Input.GetAxisRaw("Vertical");

        // Verifica se está se movendo em qualquer direção
        if (playerMovementDirection.sqrMagnitude > 0.01f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                if (pisouEsquerda)
                    SoundManager.Instance.PlayPassoEsquerda();
                else
                    SoundManager.Instance.PlayPassoDireita();

                pisouEsquerda = !pisouEsquerda;
                stepTimer = stepDelay;
            }
        }
        else
        {
            // Se parou de andar, reseta o timer e volta para o "passo esquerdo"
            stepTimer = 0f;
            pisouEsquerda = true;
        }
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
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && GameManager.Instance.dashAvailable)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("Dashing");
        trailRenderer.emitting = true;
        Invoke(nameof(DisableTrail), 0.2f);
        SoundManager.Instance.PlaySomDash();
        canDash = false;
        isDashing = true;
        rb.linearVelocity = playerMovementDirection.normalized * dashPower;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        runesUI.StartDashCooldown();
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    void DisableTrail() => trailRenderer.emitting = false;

}
