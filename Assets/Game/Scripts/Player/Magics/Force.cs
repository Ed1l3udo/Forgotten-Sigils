using UnityEngine;

public class Force : BaseMagic
{
    private GameObject forceballPrefab;
    private float speed = 10f;

    public Force(GameObject prefab, int manaCost) : base(manaCost)
    {
        forceballPrefab = prefab;
    }
    
    public override void Cast(Transform caster, Vector3 targetPosition){
        Vector3 direction = (targetPosition - caster.position).normalized;

        GameObject forceball = Object.Instantiate(forceballPrefab, caster.position + (direction * 2f), Quaternion.identity);
        Rigidbody2D rb = forceball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        forceball.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
