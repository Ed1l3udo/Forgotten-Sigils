using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayVolume : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderIntensityText;

    private Slider slider;

    void Start()
    {
        // Pega o componente Slider no mesmo GameObject
        slider = GetComponent<Slider>();

        // Atualiza o texto inicial
        UpdateSliderText(slider.value);

        // Atualiza o texto sempre que o valor do slider mudar
        slider.onValueChanged.AddListener(UpdateSliderText);
    }

    private void UpdateSliderText(float value)
    {
        // Mostra o valor como inteiro
        sliderIntensityText.text = ((int)value).ToString();
    }

}
