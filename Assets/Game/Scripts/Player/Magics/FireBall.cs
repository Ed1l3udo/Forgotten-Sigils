using UnityEngine;

// Classe da magia de bola de fogo
public class FireBall : BaseMagic
{
    private GameObject fireballPrefab;
    private float speed = 10f;
    public float offset = 1f;

    public FireBall(GameObject prefab, int manaCost) : base(manaCost)
    {
        fireballPrefab = prefab;
    }

    public override void Cast(Transform caster, Vector3 targetPosition){
        Vector3 direction = (targetPosition - caster.position).normalized;

        GameObject fireball = Object.Instantiate(fireballPrefab, caster.position + (direction * offset), Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireball.transform.rotation = Quaternion.Euler(0, 0, angle+90);
    }
}

