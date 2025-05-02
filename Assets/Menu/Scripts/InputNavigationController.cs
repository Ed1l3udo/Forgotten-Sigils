using UnityEngine;
using UnityEngine.EventSystems;

public class InputNavigationController : MonoBehaviour
{
    public GameObject defaultButton; // botão inicial do menu
    private bool usingKeyboard = true;
    private GameObject lastSelected;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(defaultButton);
        lastSelected = defaultButton;
    }

    void Update()
    {
        DetectInputMethod();
        ApplyNavigationRules();
    }

    public void ForceKeyboardControl(GameObject focusObject)
    {
        usingKeyboard = true;
        lastSelected = focusObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(focusObject);
    }

    private void DetectInputMethod()
    {
        // Detecta movimentação do mouse
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.anyKeyDown && IsMouseClick())
        {
            if (usingKeyboard)
            {
                usingKeyboard = false;
                EventSystem.current.SetSelectedGameObject(null); // remove seleção de teclado
            }
        }

        // Detecta navegação por teclado
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            if (!usingKeyboard)
            {
                usingKeyboard = true;
                EventSystem.current.SetSelectedGameObject(lastSelected ?? defaultButton); // volta foco pro último botão
            }
        }
    }

    private void ApplyNavigationRules()
    {
        if (usingKeyboard)
        {
            // Atualiza o último botão selecionado
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                lastSelected = EventSystem.current.currentSelectedGameObject;
            }
            else if (lastSelected != null)
            {
                // Garante que algo está selecionado
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
    }

    private bool IsMouseClick()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }
}