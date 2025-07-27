using UnityEngine;

public class MeleeMagic : BaseMagic
{
    private GameObject meleePrefab;
    private float distance = 1f;

    public MeleeMagic(GameObject prefab, int manaCost) : base(manaCost)
    {
        meleePrefab = prefab;
    }

    public override void Cast(Transform caster, Vector3 targetCastingPosition)
    {
        Vector3 direction = (targetCastingPosition - caster.position).normalized;
        Vector3 spawnPosition = caster.position + direction * distance;

        GameObject slash = Object.Instantiate(meleePrefab, spawnPosition, Quaternion.identity);

        // Gira para ficar voltado para o mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slash.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
