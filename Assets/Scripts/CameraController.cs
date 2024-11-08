using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraSpeed = 2;
    private float aheadDistance = 2.5f;
    private float lookAhead;

    [SerializeField] private Transform player;

    

    
    void Update()
    {
        // make the camera follow the player's position on every frame
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(
            lookAhead,
            (aheadDistance * player.localScale.x),
            Time.deltaTime * cameraSpeed
        );
    }
}
