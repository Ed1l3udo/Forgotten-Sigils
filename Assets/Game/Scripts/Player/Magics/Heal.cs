using UnityEngine;
using System.Collections;

public class Heal : BaseMagic
{
    private MonoBehaviour casterMono;
    private GameObject healParticlePrefab;
    private float healAmount = 1f;
    private float channelDuration = 3f;
    private bool isHealing = false;

    public Heal(MonoBehaviour mono, int manaCost, GameObject particlePrefab) : base(manaCost)
    {
        casterMono = mono;
        healParticlePrefab = particlePrefab;
    }

    public override void Cast(Transform caster, Vector3 targetCastingPosition)
    {
        if (!isHealing)
        {
            casterMono.StartCoroutine(ChannelHeal(caster));
        }
    }

    private IEnumerator ChannelHeal(Transform caster)
    {
        PlayerHealth health = caster.GetComponent<PlayerHealth>();
        PlayerMovement movement = caster.GetComponent<PlayerMovement>();
        isHealing = true;
        GameObject efeito = null;
        float timer = 0f;

        if (healParticlePrefab != null)
        {
            efeito = Object.Instantiate(healParticlePrefab, caster.position, Quaternion.identity, caster);
            Debug.Log("Partícula de cura instanciada em: " + efeito.transform.position);
        }
        // Pode adicionar feedback visual aqui (ex: animação, partícula)
        Debug.Log("Canalizando cura...");

        health.foiAtacadoDuranteCura = false;
        movement.canMove = false;

        while (timer < channelDuration)
        {
            timer += Time.deltaTime;

            // Interromper se o jogador soltar F (ou se mover, tomar dano, etc. - opcional)
            if (!Input.GetKey(KeyCode.F) || health.foiAtacadoDuranteCura)
            {
                Debug.Log("Cura cancelada!");
                if (efeito != null) Object.Destroy(efeito);
                movement.canMove = true;
                isHealing = false;
                yield break;
            }

            yield return null;
        }

       
        if (health != null)
        {
            health.Heal((int)healAmount);
            Debug.Log("Curado com sucesso!");
        }

        if (efeito != null)
        {
            Object.Destroy(efeito);
            Debug.Log("Partícula de cura destruida");
        }

        movement.canMove = true;
        isHealing = false;
    }
}
