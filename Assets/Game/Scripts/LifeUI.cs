using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite hollowHeart;
    [SerializeField] private int maxLife;

    private int currentLife;

    void Start()
    {
        currentLife = maxLife;
        UpdateUI();
    }

    public void TakeDamage()
    {
        currentLife--;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife); // doesn't let currentLife go beyond 0 or maxlife
        UpdateUI();
    }

    public void Heal(int healing)
    {
        currentLife += healing;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife); // doesn't let currentLife go beyond 0 or maxlife
        UpdateUI();
    }

    void UpdateUI()
    {
        for(int i = 1; i < hearts.Length * 2; i+=2)
        {
            if(i < currentLife)
            {
                hearts[i/2].sprite = fullHeart;
            }
            else
            {
                if(i-1 < currentLife)
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
