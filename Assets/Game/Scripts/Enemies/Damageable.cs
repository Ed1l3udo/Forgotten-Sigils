using UnityEngine;

// Classe genérica para qualquer monstro que for tomar dano
public abstract class Damageable : MonoBehaviour
{
    public ParticleSystem deathEffect; 

    protected virtual void Start()
    {
        if (deathEffect == null)
        {
            deathEffect = Resources.Load<ParticleSystem>("DeathSmokeEffect"); 
        }

        if (deathEffect == null)
        {
            Debug.LogWarning("Efeito de morte não atribuído!");
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
}
