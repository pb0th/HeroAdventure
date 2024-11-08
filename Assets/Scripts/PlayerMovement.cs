using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5;
    private float jumpSpeed = 7;
    private Rigidbody2D body;
    private Animator animator;

    private bool grounded;

    private float horizontalInput;
    private PlayerAttack playerAttack; 

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        // horizontalInput = Input.GetAxis("Horizontal");
    }


    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Only allow movement if not attacking
        if (playerAttack == null || !playerAttack.IsAttacking())
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            // Handle flipping player when moving left or right
            Vector3 currentScale = transform.localScale;
            if (horizontalInput > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            }
            animator.SetBool("Run", horizontalInput != 0);
        }

        if (Input.GetKey(KeyCode.Space) && grounded && (playerAttack == null || !playerAttack.IsAttacking()))
        {
            OnJump();
        }

        
        animator.SetBool("Grounded", grounded);
    }


    private void OnJump() {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        grounded = false;
    }


     private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }

    public bool CanAttack() {
        return horizontalInput == 0 && grounded;
    }
}
