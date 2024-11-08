using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    private float speed = 10f;
    public Rigidbody2D rigidbody2D;
    private Animator animator;
    [SerializeField] private float projectileDamage = 3;
    private bool goThrough = false;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

     private void Start()
    {
        // Set a timer to destroy the projectile after 3 seconds if no collision occurs
        Invoke("DestroyProjectile", 3f); 
    }



    // Call this method to set the direction
    public void SetDirection(float direction, bool _goThrough = false)
    {
        goThrough = _goThrough;
        rigidbody2D.linearVelocity = new Vector2(direction * speed, 0); // Set velocity based on direction
        Debug.Log("LINEAR VELOCITY" + rigidbody2D.linearVelocity);

    }

     // Called when the projectile collides with another object
    private void OnTriggerEnter2D(Collider2D collider)

    {
        if (collider.CompareTag("Projectile"))
        {
            Debug.Log("Collided with another projectile, ignoring...");
            return;
        }

        if (collider.CompareTag("Enemy"))
        {   
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
 
            enemyHealth.OnTakeDamage(projectileDamage);
        }
       if(!collider.CompareTag("Ground")) {
            // Stop the projectile's movement
            if(!goThrough) {
                rigidbody2D.linearVelocity = Vector2.zero;
            }
            rigidbody2D.isKinematic = true;  
            animator.SetTrigger("Explode");
       }


    }


    public void DestroyProjectile() {
        Destroy(gameObject); 
    }
}
