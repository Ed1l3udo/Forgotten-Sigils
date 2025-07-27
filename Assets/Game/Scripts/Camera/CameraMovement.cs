using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // object to follow
    private Vector3 offset;        // distance from target to camera
    public float Speed = 5f; // Velocidade de suavização do movimento

    void Start()
    {
        transform.position = new Vector3 (GameManager.Instance.playerPosition.x, GameManager.Instance.playerPosition.y, -10f); 
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
