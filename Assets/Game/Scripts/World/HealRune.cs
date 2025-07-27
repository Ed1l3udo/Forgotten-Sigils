using UnityEngine;

public class HealRune : MonoBehaviour
{
    public GameObject particleEffect;
    private GameObject efeitoAtivo;
    public RunesUI runesUI;

    public int healManaCost = 5;
    public float floatHeight = 0.5f;  
    public float floatSpeed = 2f;     
    private Vector3 startPosition;
    private bool collided = false;

    void Start()
    {
        if (GameManager.Instance.forceAvailable) Destroy(gameObject);
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        float scale = 1 + Mathf.Sin(Time.time * floatSpeed) * 0.1f; 
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!collided && other.gameObject.CompareTag("Player"))
        {
            collided = true;
            GameManager.Instance.healAvailable = true;
            Destroy(gameObject, 4f);
            if (particleEffect != null) efeitoAtivo = Instantiate(particleEffect, transform.position, Quaternion.identity);
            runesUI.UpdateRunes();
        }

    }
}
