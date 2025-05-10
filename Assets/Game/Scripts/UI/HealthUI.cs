using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite hollowHeart;

    public void UpdateUI(int currentHealth)
    {
        for(int i = 1; i < hearts.Length * 2; i+=2)
        {
            if(i < currentHealth)
            {
                hearts[i/2].sprite = fullHeart;
            }
            else
            {
                if(i-1 < currentHealth)
                {
                    hearts[i/2].sprite = halfHeart;
                }
                else
                {
                    hearts[i/2].sprite = hollowHeart;
                }
            }
        }
    }
}
