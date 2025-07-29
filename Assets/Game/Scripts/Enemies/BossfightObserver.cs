using UnityEngine;
using System.Collections;

public class BossfightObserver : MonoBehaviour
{
    public GameObject kingOrc;
    public float tamanhoQuandoDentro = 7f;
    public float tamanhoOriginal = 5f;
    public float velocidadeDeTransicao = 2f;

    private Camera cam;
    private float tamanhoDesejado;
    private bool triggered = false;

    private void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Câmera principal não encontrada.");
            return;
        }

        tamanhoDesejado = cam.orthographicSize;

        GetComponent<BoxCollider2D>().enabled = true;

        if (GameManager.Instance.deadOrc) Destroy(gameObject);
    }

    private void Update()
    {
        if (cam != null)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, tamanhoDesejado, Time.deltaTime * velocidadeDeTransicao);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(AtrasarTrigger());
        }
    }

    private IEnumerator AtrasarTrigger()
    {
        if (cam != null) tamanhoDesejado = tamanhoQuandoDentro;
        yield return new WaitForSeconds(2f);

        if (kingOrc != null)
        {
            kingOrc.GetComponent<KingOrc>().StartFight();
        }

        GetComponent<BoxCollider2D>().enabled = false;


        yield return new WaitForSeconds(3f);

        if (cam != null) tamanhoDesejado = tamanhoOriginal;
    }
}
