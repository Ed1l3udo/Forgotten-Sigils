using UnityEngine;

public class ForceBallProjectile : MonoBehaviour
{
    public float lifetime = 20f;

    void Start()
    {
        // Destroi automaticamente após 'lifetime' segundos -> evitar erros
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Tenta encontrar um componente-alvo que implemente Damageable
        Movable alvo = collision.gameObject.GetComponent<Movable>();

        if(alvo != null) alvo.EnterMoveState();

        // Destroi ao colidir com qualquer coisa
        Destroy(gameObject);
    }
}
