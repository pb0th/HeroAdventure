using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    public float damage = 4;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {   
        Debug.Log("Collided with: " + collider.gameObject.name);
        if (collider.gameObject.tag == "Enemy")
        {   
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
 
            enemyHealth.OnTakeDamage(damage);
        }
    }
}
