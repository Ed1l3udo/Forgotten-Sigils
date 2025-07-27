using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RunesUI : MonoBehaviour
{
    public Image[] runeSlots;      // Slots visuais (Image UI)
    public Sprite[] runeIcons;     // Sprites das runas
    public string[] associatedMagicClassNames; // Nomes das classes de magia: "FireBall", "WindBlast", etc.
    public Image dashCooldownOverlay;
    private List<BaseMagic> magics = new List<BaseMagic>();
    public GameObject fireballPrefab;  // Referência ao prefab da Fireball
    public GameObject forceballPrefab;
    public GameObject windBlastPrefab;
    public float dashCooldownDuration = 1f;

    void Start()
    {
        UpdateRunes();
    }

    public void UpdateRunes()
    {
        AtualizarMagias();
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

    public void HighlightRune(int selectedIndex)
    {
        for (int i = 1; i < runeSlots.Length; i++)
        {
            Image img = runeSlots[i];

            bool estaDesbloqueada = img.sprite != null;

            if (!estaDesbloqueada)
            {
                // Runa ainda não foi desbloqueada → deixa invisível ou com alpha 0
                img.color = new Color(1f, 1f, 1f, 0f);
                img.transform.localScale = Vector3.one;
                continue;
            }

            if (i == selectedIndex)
            {
                // Destaque da runa selecionada
                img.color = new Color(1f, 1f, 1f, 1f); // brilho total
                img.transform.localScale = Vector3.one * 1.2f;
            }
            else
            {
                // Runa desbloqueada mas não selecionada
                img.color = new Color(1f, 1f, 1f, 0.7f);
                img.transform.localScale = Vector3.one;
            }
        }
    }

    public void StartDashCooldown()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateDashCooldown());
    }

    private IEnumerator AnimateDashCooldown()
    {
        float timer = 0f;
        dashCooldownOverlay.fillAmount = 1f;

        while (timer < dashCooldownDuration)
        {
            timer += Time.deltaTime;
            dashCooldownOverlay.fillAmount = 1f - (timer / dashCooldownDuration);
            yield return null;
        }

        dashCooldownOverlay.fillAmount = 0f;
    }
    
        public void AtualizarMagias()
    {
        if (GameManager.Instance.fireAvailable &&
            !magics.Exists(m => m is FireBall))
        {
            magics.Add(new FireBall(fireballPrefab, 3));
        }

        if (GameManager.Instance.windAvailable &&
            !magics.Exists(m => m is WindBlast))
        {
            magics.Add(new WindBlast(windBlastPrefab, 7));
        }

        if (GameManager.Instance.forceAvailable &&
            !magics.Exists(m => m is Force))
        {
            magics.Add(new Force(forceballPrefab, 9));
        }
    }
}
