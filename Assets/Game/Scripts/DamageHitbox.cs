using UnityEngine;

public class DamageHitbox : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.TakeDamage(1);
            }
            Debug.Log("Encostou no jogador");
        }
    }
}
