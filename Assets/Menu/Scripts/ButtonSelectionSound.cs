using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public AudioController audioController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioController != null && GetComponent<Slider>() == null)
        {
            audioController.PlaySelectionSound();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (audioController != null)
        {
            audioController.PlaySelectionSound();
        }
    }
}