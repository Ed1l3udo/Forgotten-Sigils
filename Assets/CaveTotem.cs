using UnityEngine;

public class CaveTotem : MonoBehaviour
{
    [SerializeField] private GameObject leftAreaDetector;
    [SerializeField] private GameObject rightAreaDetector;

    public GameObject particlePrefab;
    private GameObject efeitoAtivo;

    private MovableAreaDetector leftDetector;
    private MovableAreaDetector rightDetector;

    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRend;
    public bool active = false;

    void Start()
    {
        leftDetector = leftAreaDetector.GetComponent<MovableAreaDetector>();
        rightDetector = rightAreaDetector.GetComponent<MovableAreaDetector>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (leftDetector.active && rightDetector.active)
        {
            active = true;
            spriteRend.sprite = sprites[3];
            if (efeitoAtivo == null) efeitoAtivo = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
        else if (leftDetector.active)
        {
            spriteRend.sprite = sprites[1];
            if (efeitoAtivo != null) Destroy(efeitoAtivo);
        }
        else if (rightDetector.active)
        {
            spriteRend.sprite = sprites[2];
            if (efeitoAtivo != null) Destroy(efeitoAtivo);
        }
        else
        {
            spriteRend.sprite = sprites[0];
            if (efeitoAtivo != null) Destroy(efeitoAtivo);
        }
    }
}
