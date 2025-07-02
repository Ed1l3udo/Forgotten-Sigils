using UnityEngine;

public abstract class BaseMagic : MonoBehaviour
{
    public int ManaCost{ get; private set; }

    public BaseMagic(int manaCost){
        ManaCost = manaCost;
    }

    public abstract void Cast(Transform caster, Vector3 targetCastingPosition);
    // Classe base de magia generica com um caster (normalmente o player) e uma posição de casting
}
