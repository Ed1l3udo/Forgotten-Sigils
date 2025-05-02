using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonJump : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        JumpUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        JumpDown();
    }

    public void OnSelect(BaseEventData eventData)
    {
        JumpUp();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        JumpDown();
    }

    public void JumpUp()
    {
        // Faz o botão subir suavemente
        LeanTween.moveLocalY(gameObject, originalPosition.y + 20f, 0.2f).setEaseOutQuad();
    }

    public void JumpDown()
    {
        // Volta o botão para a posição original
        LeanTween.moveLocalY(gameObject, originalPosition.y, 0.2f).setEaseInQuad();
    }
}