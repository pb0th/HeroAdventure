using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 10;
    public float health;
    private Animator animator;

    private bool isDead = false;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    public void OnTakeDamage(float damage) {
        if (isDead) return; 
        animator.SetTrigger("Hurt");
        health -= damage;

        if (health <= 0)
        {
            health = 0; 
            Die();
        }
    }

    // Method to handle enemy death
    private void Die()
    {
        isDead = true; // Mark as dead to avoid further damage handling
        animator.SetTrigger("Dead"); // Trigger death animation
        Invoke("RemoveEnemy", 1f); // Delay before removing the enemy
    }

    // Method to remove the enemy from the scene
    private void RemoveEnemy()
    {
        Destroy(gameObject);
    }

}
