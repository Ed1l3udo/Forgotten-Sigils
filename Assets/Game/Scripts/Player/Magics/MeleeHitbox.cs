using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public int dano = 1;
    public float lifeTime = 0.2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable alvo = other.gameObject.GetComponent<Damageable>();

        if (alvo != null) alvo.TakeDamage(dano);
    }
}
