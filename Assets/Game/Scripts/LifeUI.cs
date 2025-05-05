using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
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
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < currentLife)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = hollowHeart;
            }
        }
    }
}
