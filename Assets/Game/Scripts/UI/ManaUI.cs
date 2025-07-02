using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private Image[] orbs;
    [SerializeField] private Sprite fullOrb;
    [SerializeField] private Sprite halfOrb;
    [SerializeField] private Sprite emptyOrb;

    public void UpdateUI(int currentMana)
    {
        for (int i = 1; i < orbs.Length * 2; i += 2)
        {
            if (i < currentMana)
            {
                orbs[i / 2].sprite = fullOrb;
            }
            else
            {
                if (i - 1 < currentMana)
                {
                    orbs[i / 2].sprite = halfOrb;
                }
                else
                {
                    orbs[i / 2].sprite = emptyOrb;
                }
            }
        }
    }
}
