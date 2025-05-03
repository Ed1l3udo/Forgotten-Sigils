using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderSelectionHighlight : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    [SerializeField] private Image outlineImage;

    void Start()
    {
        outlineImage.enabled = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        outlineImage.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        outlineImage.enabled = false;
    }
}
