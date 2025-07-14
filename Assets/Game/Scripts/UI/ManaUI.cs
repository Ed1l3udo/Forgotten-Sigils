using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private Image[] manaBar;
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite middle1;
    [SerializeField] private Sprite middle2;
    [SerializeField] private Sprite finishSprite;

    private void UpdateManaBar(bool show, int index){
        if (index < 0 || index >= manaBar.Length) return;

        if (show)
        {
            if (index == 0) manaBar[index].sprite = startSprite;
            else if (index == manaBar.Length - 1) manaBar[index].sprite = finishSprite;
            else if (index % 2 == 0) manaBar[index].sprite = middle1;
            else manaBar[index].sprite = middle2;
        }
        else
        {
            manaBar[index].sprite = null; 
        }
    }

    public void UpdateUI(int currentMana)
    {
        for (int i = 1; i < manaBar.Length; ++i)
        {
            bool show = (currentMana > 0);
            UpdateManaBar(show, i);

            currentMana--;
        }
    }
}
