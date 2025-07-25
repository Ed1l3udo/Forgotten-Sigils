using UnityEngine;
using System.Collections;

public abstract class Damageable : MonoBehaviour
{
    public ParticleSystem deathEffect;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool knockedBack = false;

    protected virtual void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer (no objeto ou filhos) não encontrado!");
        }
        else
        {
            originalColor = spriteRenderer.color;
        }

        if (deathEffect == null)
        {
            deathEffect = Resources.Load<ParticleSystem>("DeathSmokeEffect");
        }
    }

    public abstract void TakeDamage(int amount);

    public void Die()
    {
        if (deathEffect != null)
        {
            ParticleSystem effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, effect.main.duration + 0.5f);
        }

        Destroy(gameObject);
    }

    protected void ShowDamageEffect()
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashColor(Color.red, 0.2f));
        }
    }

    private IEnumerator FlashColor(Color flashColor, float duration)
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }

    public void ApplyKnockBack()
    {
        StartCoroutine(KnockBack());
    }

    private IEnumerator KnockBack()
    {
        knockedBack = true;
        yield return new WaitForSeconds(1f);
        knockedBack = false;
    }
}
