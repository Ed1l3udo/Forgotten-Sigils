using UnityEngine;

public class GiganticRock : MonoBehaviour
{
    public GameObject totem1;
    public GameObject totem2;
    public GameObject bigTotem;
    public Sprite movedSprite;
    private SpriteRenderer spRend;

    public bool moved;

    private Vector3 posicaoOriginal = new Vector3(7, 30, 0);
    private Vector3 posicaoFinal = new Vector3(7, 26, 0);
    private float velocidade = 3f;

    void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
        moved = GameManager.Instance.rockMoved;

        if (moved)
        {
            transform.position = posicaoFinal;
            spRend.sprite = movedSprite;
        }
        else
        {
            transform.position = posicaoOriginal;
        }
    }

    void Update()
    {
        if (!moved && totem1.GetComponent<CaveTotem>().active &&
                     totem2.GetComponent<CaveTotem>().active &&
                     bigTotem.GetComponent<CaveTotem>().active)
        {
            moved = true;
            GameManager.Instance.rockMoved = true;
            spRend.sprite = movedSprite;
        }

        Vector3 destino = moved ? posicaoFinal : posicaoOriginal;
        transform.position = Vector3.Lerp(transform.position, destino, Time.deltaTime * velocidade);
    }
}
