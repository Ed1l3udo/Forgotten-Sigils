using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, ISelectHandler
{
    public AudioController audioController;

    public void OnSelect(BaseEventData eventData)
    {
        if (audioController != null)
        {
            audioController.PlaySelectionSound();
        }
    }
}