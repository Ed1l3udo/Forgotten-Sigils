using UnityEngine;

public class Force : BaseMagic
{
    private GameObject beamPrefab;
    private float beamDuration = 0.2f;

    public Force(GameObject prefab, int manaCost) : base(manaCost)
    {
        beamPrefab = prefab;
    }

    public override void Cast(Transform caster, Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - caster.position;
        float distance = direction.magnitude;
        direction.Normalize();

        // Verifica alvo no ponto do clique
        Collider2D hitCollider = Physics2D.OverlapCircle(targetPosition, 0.1f);
        if (hitCollider != null)
        {
            Movable alvo = hitCollider.GetComponent<Movable>();
            if (alvo != null)
            {
                alvo.EnterMoveState(); 
            }
        }

        // Instancia o feixe na posição do caster
        GameObject beam = Object.Instantiate(beamPrefab, caster.position, Quaternion.identity);

        // Escala o feixe no eixo X (deve ser orientado da esquerda para a direita)
        beam.transform.localScale = new Vector3(distance, beam.transform.localScale.y, 1f);

        // Rotaciona o feixe
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        beam.transform.rotation = Quaternion.Euler(0, 0, angle);


        // Destroi após o tempo
        Object.Destroy(beam, beamDuration);
    }
}
