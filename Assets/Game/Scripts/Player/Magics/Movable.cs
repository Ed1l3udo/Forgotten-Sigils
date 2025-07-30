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
    public GameObject particlePrefab;
    private GameObject efeitoAtivo;
    public int number;

    [SerializeField] private Sprite staticSprite;
    [SerializeField] private Sprite movingSprite;

    void Start()
    {
        if(player == null) player = GameObject.FindGameObjectWithTag("Player");

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

        efeitoAtivo = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        efeitoAtivo.transform.SetParent(transform);

        float tempoDecorrido = 0f;

        while (tempoDecorrido < tempo)
        {
            playerM.canMove = false;

            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape)) && tempoDecorrido > 0.3f) break;

            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        if (efeitoAtivo != null) Destroy(efeitoAtivo);

        playerM.canMove = true;
        canMove = false;
        spRend.sprite = staticSprite;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (!canMove) return;

        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (movementDirection.sqrMagnitude > 0.01f) rb.linearVelocity = movementDirection.normalized * moveSpeed;
        else rb.linearVelocity = Vector2.zero;
    }
}
