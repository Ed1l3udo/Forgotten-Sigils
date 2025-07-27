using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    [SerializeField] private Sprite[] hearts;
    [SerializeField] private Image heartImage;

    public void UpdateUI(int currentHealth)
    {
        heartImage.sprite = hearts[currentHealth];
    }
}
