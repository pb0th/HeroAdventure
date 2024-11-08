using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private Animator animator;
    public float damage = 2; 
    
    [SerializeField] private GameObject sword;
    private Collider2D swordCollider;
    
    


    private void Awake() {
        animator = GetComponent<Animator>();
        swordCollider = sword.GetComponent<Collider2D>();

        // Initially disable sword collider
        swordCollider.enabled = false;
    }
    
    public void AttackPlayer() {
        animator.SetTrigger("Attack");

    }


    public void EnableSwordCollider() {
        // Debug.Log("Enable collider");
        swordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        // Debug.Log("Disable Collider");
        swordCollider.enabled = false;
    }
}
