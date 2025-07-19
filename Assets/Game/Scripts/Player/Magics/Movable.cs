using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
    public GameObject player;
    public float moveTime = 10f;
    public float moveSpeed = 3f;
    private Vector2 movementDirection;
    private bool canMove = false;
    private Rigidbody2D rb;
    private PlayerMovement playerM;
    private SpriteRenderer spRend;

    [SerializeField] private Sprite staticSprite;
    [SerializeField] private Sprite movingSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spRend = GetComponent<SpriteRenderer>();
        playerM = player.GetComponent<PlayerMovement>(); 
    }

    public void EnterMoveState()
    {
        StartCoroutine(ExecutarPorTempo(moveTime));
    }

    IEnumerator ExecutarPorTempo(float tempo)
    {
        playerM.canMove = false;      
        canMove = true;                
        spRend.sprite = movingSprite;
        rb.bodyType = RigidbodyType2D.Dynamic;

        float tempoDecorrido = 0f;

        while (tempoDecorrido < tempo){
            
            if (Input.GetMouseButtonDown(0)) break;
            
            tempoDecorrido += Time.deltaTime;
            yield return null; 
        }

        playerM.canMove = true;        
        canMove = false;
        spRend.sprite = staticSprite;
        rb.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        if (!canMove) return;

        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        if (movementDirection.sqrMagnitude > 0.01f) rb.linearVelocity = movementDirection.normalized * moveSpeed;
        else rb.linearVelocity = Vector2.zero;
    }
}
