using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    public float lifetime = 20f;
    public int dano = 10;

    void Start()
    {
        // Destroi automaticamente após 'lifetime' segundos -> evitar erros
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Tenta encontrar um componente-alvo que implemente Damageable
        Damageable alvo = collision.gameObject.GetComponent<Damageable>();

        if(alvo != null) alvo.TakeDamage(dano);

        // Destroi ao colidir com qualquer coisa
        Destroy(gameObject);
    }
}
