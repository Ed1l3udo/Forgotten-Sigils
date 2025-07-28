using UnityEngine;
using TMPro;

public class DialogoUIConnector : MonoBehaviour
{
    void Start()
    {
        GameObject painel = GameObject.Find("PainelDialogo");
        TMP_Text texto = GameObject.Find("TextoDialogo")?.GetComponent<TMP_Text>();

        if (painel != null && texto != null && DialogoManager.Instance != null)
        {
            DialogoManager.Instance.ConfigurarUI(painel, texto);
        }
    }
}