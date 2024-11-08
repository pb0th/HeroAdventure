using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image healthBar;
    public float maxHealth = 30;
    public float health;
    private Animator animator;


    private bool isDead = false;

    private void Awake() {
        health = maxHealth;
        animator = GetComponent<Animator>();
        
    }


    public void OnHeal(float healAmount) {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth; 
        }
    }

    public void OnTakeDamage(float damage) {
        if(!isDead) {
            animator.SetTrigger("Hurt");
            health -= damage;


            if (health <= 0)
            {
                health = 0; 
                Die();
            } 
            UpdateHealthBar();
        }
        // Debug.Log("Player took damage. Current health: " + health);
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die"); 

        PlayerMovement movementScript = GetComponent<PlayerMovement>();
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }



        // Restart the game after a delay
        Invoke("RestartGame", 4f);
    }


    private void UpdateHealthBar()
    {
        // Calculate the fill amount as a percentage of current health
        healthBar.fillAmount = health / maxHealth;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the current scene
    }

    
}
