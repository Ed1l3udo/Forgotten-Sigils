using System;
using UnityEngine;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager Instance;

    public GameObject painelDialogo;         // Painel da caixa de diálogo
    public TMP_Text textoDialogo;            // Texto principal
    public TMP_Text nomePersonagemTexto;     // (Opcional) Nome do personagem, se usar

    private string[] falas;
    private int index;
    private bool dialogoAtivo;
    private Action onDialogEnd;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ConfigurarUI(GameObject painel, TMP_Text texto)
    {
        painelDialogo = painel;
        textoDialogo = texto;
    }

    public void IniciarDialogo(string[] novasFalas, Action aoFinalizar = null)
    {
        if (novasFalas == null || novasFalas.Length == 0) return;

        falas = novasFalas;
        index = 0;
        onDialogEnd = aoFinalizar;

        painelDialogo.SetActive(true);
        MostrarFalaAtual();
        dialogoAtivo = true;
    }

    void Update()
    {
        if (dialogoAtivo && Input.GetKeyDown(KeyCode.E))
        {
            index++;
            if (index < falas.Length)
            {
                MostrarFalaAtual();
            }
            else
            {
                EncerrarDialogo();
            }
        }
    }

    void MostrarFalaAtual()
    {
        textoDialogo.text = falas[index];
    }

    void EncerrarDialogo()
    {
        painelDialogo.SetActive(false);
        dialogoAtivo = false;

        onDialogEnd?.Invoke();
        onDialogEnd = null;
    }

    // Se quiser usar nome de personagem:
    public void SetarNomePersonagem(string nome)
    {
        if (nomePersonagemTexto != null)
            nomePersonagemTexto.text = nome;
    }
}
