using UnityEngine;

public class FireRune : MonoBehaviour
{
    public GameObject particleEffect;
    private GameObject efeitoAtivo;
    public GameObject fireBallPrefab;
    public int fireBallManaCost = 3;
    public float floatHeight = 0.5f;  
    public float floatSpeed = 2f;     
    private Vector3 startPosition;
    private bool collided = false;

    void Start()
    {
        if (GameManager.Instance.fireAvailable) Destroy(gameObject);
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
            GameManager.Instance.availableMagics.Add(new FireBall(fireBallPrefab, fireBallManaCost));
            GameManager.Instance.fireAvailable = true;
            Destroy(gameObject, 4f);
            if (particleEffect != null) efeitoAtivo = Instantiate(particleEffect, transform.position, Quaternion.identity);
        }

    }
}
