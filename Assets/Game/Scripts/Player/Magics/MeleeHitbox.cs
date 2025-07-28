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
        // Dano em inimigos ou alvos
        Damageable alvo = other.GetComponent<Damageable>();
        if (alvo != null)
        {
            alvo.TakeDamage(dano);
        }

        // Quebra de tiles
        if (other.gameObject.CompareTag("TileMapBreakable"))
        {
            BreakableTile destructible = other.GetComponent<BreakableTile>();
            if (destructible != null)
            {
                destructible.DestroyAllTiles();
                GameManager.Instance.caveWallHasBeenBroken = true;
            }
        }
    }
}
