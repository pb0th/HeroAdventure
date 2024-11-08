using UnityEngine;

public class EnemySword : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    public float damage = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {   
            playerHealth.OnTakeDamage(damage);
        }
    }
}
