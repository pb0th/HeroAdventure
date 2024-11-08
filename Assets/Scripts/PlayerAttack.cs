using UnityEngine;
using UnityEngine.UI;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform ultFirePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject ultimateProjectilePrefab;
    [SerializeField] private Image powerBar;

    [SerializeField] private float maxPower = 100;
    private float currentPower;
    [SerializeField] private float powerRegenerationRate = 5f; 

    private float projectileCooldown = 0.5f;
    private float projectileCooldownTimer = Mathf.Infinity;

    private int attackStep = 0;  // Tracks the current attack in the combo
    private float comboResetTimer;
    [SerializeField] private float comboResetTime = 1f;
    private bool isAttacking = false; 

    private Animator animator;
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject sword;
    private Collider2D swordCollider;

    private void Awake() {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        swordCollider = sword.GetComponent<Collider2D>();

        // Initially disable sword collider
        swordCollider.enabled = false;
        currentPower = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && projectileCooldownTimer > projectileCooldown && playerMovement.CanAttack()) {
            OnProjectileShoot();
        }
        if (Input.GetKeyDown(KeyCode.W) && playerMovement.CanAttack())
        {
            OnAttack();
        }
        if (Input.GetKeyDown(KeyCode.R) && projectileCooldownTimer > projectileCooldown && playerMovement.CanAttack())
        {
            OnUltimateShoot();
        }
        projectileCooldownTimer += Time.deltaTime;

        comboResetTimer += Time.deltaTime;

        // Reset combo if too much time passes between key presses
        if (comboResetTimer > comboResetTime)
        {
            attackStep = 0;
            ResetAttackState();
            
        }

        // Regenerate power over time if it's less than max
        if (currentPower < maxPower) {
            currentPower += powerRegenerationRate * Time.deltaTime;
            if (currentPower > maxPower) {
                currentPower = maxPower;  // Clamp to max power
            }
            UpdatePowerBar();
        }
    }

    private void OnAttack() {
        // EnableSwordCollider();
        isAttacking = true;
        comboResetTimer = 0;  // Reset combo timer whenever the key is pressed

        // Trigger different animations based on the current combo step
        switch (attackStep)
        {
            case 0:
                animator.SetTrigger("Attack_1");
                break;
            case 1:
                animator.SetTrigger("Attack_2");
                break;
            case 2:
                animator.SetTrigger("Attack_3");
                break;
        }
        // DisableSwordCollider();

        // Move to the next attack in the combo
        attackStep = (attackStep + 1) % 3;  // Loop back to 0 after 3 steps
        
    }

    private void OnProjectileShoot() {
        if(currentPower >= 20) {
            projectileCooldownTimer = 0;
            animator.SetTrigger("Shoot");
            currentPower -= 20;
            UpdatePowerBar();
        }
        
        
    }


     private void OnUltimateShoot() {
        if(currentPower == maxPower) {
            projectileCooldownTimer = 0;
            animator.SetTrigger("Ultimate");
            currentPower -= maxPower;
            UpdatePowerBar();
        }
        
        
    }

    public void CreateProjectile() {
        // Instantiate the projectile
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the PlayerProjectile component and set the direction
        PlayerProjectile projectile = projectileObject.GetComponent<PlayerProjectile>();

        // Determine direction based on player's facing direction
        float direction = transform.localScale.x > 0 ? 1f : -1f; // 1 for right, -1 for left
        projectile.SetDirection(direction);
    }

    public void CreateUltimateProjectile() {
        // Instantiate the projectile
        GameObject projectileObject = Instantiate(ultimateProjectilePrefab, ultFirePoint.position, ultFirePoint.rotation);

        // Get the PlayerProjectile component and set the direction
        PlayerProjectile projectile = projectileObject.GetComponent<PlayerProjectile>();

        // Determine direction based on player's facing direction
        float direction = transform.localScale.x > 0 ? 1f : -1f; // 1 for right, -1 for left
        projectile.SetDirection(direction, true);
    }


    // Method to reset isAttacking after the animation
    public void ResetAttackState()
    {
        isAttacking = false;
    }

    // Method to check if the player is attacking
    public bool IsAttacking()
    {
        return isAttacking;
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

    private void UpdatePowerBar() {
        // Update the fill amount of the power bar based on current power
        powerBar.fillAmount = currentPower / maxPower;
    }
}
