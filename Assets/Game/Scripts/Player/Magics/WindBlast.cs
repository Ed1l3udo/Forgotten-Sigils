using UnityEngine;

public class WindBlast : BaseMagic
{
    private float knockbackForce = 8f;
    private float radius = 4f;
    private GameObject visualEffectPrefab;

    public WindBlast(GameObject visualPrefab, int manaCost) : base(manaCost)
    {
        visualEffectPrefab = visualPrefab;
    }

    public override void Cast(Transform caster, Vector3 targetCastingPosition)
    {
        if (visualEffectPrefab != null)
        {
            GameObject.Instantiate(visualEffectPrefab, caster.position, Quaternion.identity);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(caster.position, radius);

        foreach (Collider2D hit in hits)
        {

            Damageable damageable = hit.GetComponent<Damageable>();
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (damageable != null && rb != null)
            {
                Vector2 direction = (hit.transform.position - caster.position).normalized;
                damageable.ApplyKnockBack();
                rb.linearVelocity = direction * knockbackForce;
            }

            if (hit.gameObject.CompareTag("GrassBreakable"))
            {
                Debug.Log("Detectado: " + hit.name);
                BreakableGrass destructible = hit.GetComponent<BreakableGrass>();
                if (destructible != null)
                {
                    Debug.Log("Tentando destruir");
                    destructible.DestroyAllTiles();
                    GameManager.Instance.grassHasBeenBroken = true;
                }
            }
        }
    }
}
