using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RunesUI : MonoBehaviour
{
    public Image[] runeSlots;      // Slots visuais (Image UI)
    public Sprite[] runeIcons;     // Sprites das runas
    public string[] associatedMagicClassNames; // Nomes das classes de magia: "FireBall", "WindBlast", etc.

    void Start()
    {
        UpdateRunes();
    }

    public void UpdateRunes()
    {
        List<BaseMagic> magics = GameManager.Instance.availableMagics;

        
        for (int i = 0; i < runeSlots.Length; i++)
        {
            // Esconde por padrão
            runeSlots[i].sprite = null;
            var color = runeSlots[i].color;
            color.a = 0f;
            runeSlots[i].color = color;

            if (GameManager.Instance.dashAvailable)
            {
                runeSlots[0].sprite = runeIcons[0];
                color.a = 1f;
                runeSlots[0].color = color;
            }

            // Verifica se a magia correspondente está desbloqueada
            foreach (var magic in magics)
            {
                if (magic.GetType().Name == associatedMagicClassNames[i])
                {
                    runeSlots[i].sprite = runeIcons[i];
                    color.a = 1f;
                    runeSlots[i].color = color;
                    break;
                }
            }
        }
    }
}
