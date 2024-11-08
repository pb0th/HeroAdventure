using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    
    [SerializeField]  private Transform[] patrolPoints;

    private float movementSpeed = 2;
    private int currentPatrolDestination = 0;

    [SerializeField] private Transform player;
    private bool isChasing = false;
    private bool isAttacking = false;
    private float chaseDistance = 4f;
    private float closeRangeDistance = 1.5f; 

    private EnemyDamage enemyDamage;

    private void Awake() {
        enemyDamage = GetComponent<EnemyDamage>();
    }

    void Update()
    {
        var currentScale = transform.localScale;
        

        if(isChasing) {
            float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);
            // if the player gets close enough, start attacking
            if (distanceToPlayer <= closeRangeDistance)
            {
                enemyDamage.AttackPlayer();
            }
            // else just continue chasing
            else
            {
                ChasePlayer(currentScale);        
            }
            
        }

        else {
            // patrol around the designated areas
            Patrol();
            // chase the player if the player gets too close
            if (Vector2.Distance(transform.position, player.position) < chaseDistance)
            {
                // Debug.Log("Player found. Start chasing now.");
                isChasing = true;
            }
        }
        
    }


    private void Patrol()
    {
        var currentScale = transform.localScale;

        if (currentPatrolDestination == 0)
        {
            var step = movementSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, step);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                currentPatrolDestination = 1;
                transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            }
        }
        else if (currentPatrolDestination == 1)
        {
            var step = movementSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, step);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                currentPatrolDestination = 0;
                transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            }
        }
    }

    private void ChasePlayer(Vector3 currentScale)
    {
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
        else if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
    }

    private void FacePlayer(Vector3 currentScale)
    {
        // Adjust enemy facing direction to match player's position
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
        else if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }
}
