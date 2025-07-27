using UnityEngine;
using UnityEngine.Rendering.Universal; // para Light2D

public class PlayerNightVision : MonoBehaviour
{
    public Light2D playerLight;

    void Start()
    {
        if (GameManager.Instance.hasNightVision)
        {
            playerLight.pointLightOuterRadius = 7;
        }
        else
        {
            playerLight.pointLightOuterRadius = 1;
        }
    }

    // usado quando pega a runa de luz
    public void Iluminar()
    {
        if (GameManager.Instance.hasNightVision)
        {
            playerLight.pointLightOuterRadius = 7;
        }
        else
        {
            playerLight.pointLightOuterRadius = 1;
        }
    }
}
