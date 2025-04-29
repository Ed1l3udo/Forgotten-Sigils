using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonJump : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Faz o botão subir suavemente
        LeanTween.moveLocalY(gameObject, originalPosition.y + 20f, 0.2f).setEaseOutQuad();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Volta o botão para a posição original
        LeanTween.moveLocalY(gameObject, originalPosition.y, 0.2f).setEaseInQuad();
    }
}