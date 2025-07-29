using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager Instance;

    public GameObject painelDialogo;         // Painel da caixa de diálogo
    public TMP_Text textoDialogo;            // Texto principal
    public TMP_Text nomePersonagemTexto;     // (Opcional) Nome do personagem, se usar

    public float tempoEntreLetras = 0.02f;

    private string[] falas;
    private int index;
    private bool dialogoAtivo;
    public bool digitando;
    private Coroutine coroutineDigitando;
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
        // MostrarFalaAtual();
        StartDigitarFalaAtual();
        dialogoAtivo = true;
    }

    void Update()
    {
        if (!dialogoAtivo) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (digitando)
            {
                // Pula a digitação e mostra a frase completa
                SkipToFullText();
            }
            else
            {
                // Avança para próxima fala
                index++;
                if (index < falas.Length)
                    StartDigitarFalaAtual();
                else
                    EncerrarDialogo();
            }
        }
    }

    void StartDigitarFalaAtual()
    {
        if (coroutineDigitando != null)
            StopCoroutine(coroutineDigitando);

        coroutineDigitando = StartCoroutine(DigitarTexto(falas[index]));
    }

    IEnumerator DigitarTexto(string texto)
    {
        digitando = true;
        textoDialogo.text = "";

        foreach (char letra in texto.ToCharArray())
        {
            textoDialogo.text += letra;
            // Exemplo: som de "blip" por letra
            // AudioManager.Play("blip");
            yield return new WaitForSeconds(tempoEntreLetras);
        }

        digitando = false;
    }

    void SkipToFullText()
    {
        if (coroutineDigitando != null)
            StopCoroutine(coroutineDigitando);

        textoDialogo.text = falas[index];
        digitando = false;
    }

    // void MostrarFalaAtual()
    // {
    //     textoDialogo.text = falas[index];
    // }

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
