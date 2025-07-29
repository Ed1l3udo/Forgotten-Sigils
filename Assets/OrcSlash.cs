using UnityEngine;

public class OrcSlash : MonoBehaviour
{
    public int dano = 2;
    public float lifeTime = 0.2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Dano em inimigos ou alvos
        PlayerHealth alvo = other.GetComponent<PlayerHealth>();
        if (alvo != null)
        {
            alvo.TakeDamage(dano);
        }
    }
}
