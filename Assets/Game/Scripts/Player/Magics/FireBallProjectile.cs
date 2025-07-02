using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    public float lifetime = 20f;

    void Start()
    {
        // Destroi automaticamente após 'lifetime' segundos -> evitar erros
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroi ao colidir com qualquer coisa
        Destroy(gameObject);
    }
}
