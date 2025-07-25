using UnityEngine;

public class WindBlast : BaseMagic
{
    private float knockbackForce = 8f;
    private float radius = 4f;
    private LayerMask enemyLayer;
    private GameObject visualEffectPrefab;

    public WindBlast(GameObject visualPrefab, int manaCost) : base(manaCost)
    {
        visualEffectPrefab = visualPrefab;
        enemyLayer = LayerMask.GetMask("Enemy"); // Certifique-se de que inimigos estão na Layer 'Enemy'
    }

    public override void Cast(Transform caster, Vector3 targetCastingPosition)
    {
        // Efeito visual (opcional)
        if (visualEffectPrefab != null)
        {
            GameObject.Instantiate(visualEffectPrefab, caster.position, Quaternion.identity);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(caster.position, radius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (hit.transform.position - caster.position).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
