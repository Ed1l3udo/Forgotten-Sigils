using UnityEngine;
public class EnemyController : MonoBehaviour
{

    [SerializeField] Transform player;
    public float velocity = 3f;
    void Update()
    {
        if(player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocity * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
            Debug.Log("Encostou no jogador");
        }
    }
}
