using UnityEngine;
using TMPro;

public class DialogoUIConnector : MonoBehaviour
{
    [Header("Referências da UI")]
    public GameObject painelDialogo;
    public TMP_Text textoDialogo;

    void Start()
    {
        if (painelDialogo != null && textoDialogo != null && DialogoManager.Instance != null)
        {
            DialogoManager.Instance.ConfigurarUI(painelDialogo, textoDialogo);
        }
        else
        {
            Debug.LogError("UI de diálogo não configurada no DialogoUIConnector em " + gameObject.name);
        }
    }
}
