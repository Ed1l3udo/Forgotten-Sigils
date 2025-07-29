using UnityEngine;

public class CameraAdjustTrigger : MonoBehaviour
{
    public float tamanhoQuandoDentro = 7f;
    public float tamanhoOriginal = 5f;
    public float velocidadeDeTransicao = 2f;

    private Camera cam;
    private float tamanhoDesejado;

    private void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Câmera principal não encontrada.");
            return;
        }

        tamanhoDesejado = cam.orthographicSize;
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
        if (other.CompareTag("Player") && cam != null)
        {
            tamanhoDesejado = tamanhoQuandoDentro;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cam != null)
        {
            tamanhoDesejado = tamanhoOriginal;
        }
    }
}
