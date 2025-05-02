using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // O objeto que a câmera vai seguir (ex: Player)
    private Vector3 offset;        // Distância do alvo (jogador) à câmera
    public float Speed = 5f; // Velocidade de suavização do movimento

    void Start()
    {
        // Calcula a distância inicial entre a câmera e o jogador
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // Lerp serve pra fazer com que a transição seja gradual, evitando o efeito da câmera grudada no jogador
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Speed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
