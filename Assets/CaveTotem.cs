using UnityEngine;

public class CaveTotem : MonoBehaviour
{
    [SerializeField] private GameObject leftAreaDetector;
    [SerializeField] private GameObject rightAreaDetector;

    private MovableAreaDetector leftDetector;
    private MovableAreaDetector rightDetector;

    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRend;
    private bool active = false;

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
        }
        else if (leftDetector.active) spriteRend.sprite = sprites[1];
        else if (rightDetector.active) spriteRend.sprite = sprites[2];
        else spriteRend.sprite = sprites[0];
    }
}
