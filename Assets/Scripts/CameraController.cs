using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraSpeed = 2;
    private float aheadDistance = 2.5f;
    private float lookAhead;

    [SerializeField] private Transform player;

    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(
            lookAhead,
            (aheadDistance * player.localScale.x),
            Time.deltaTime * cameraSpeed
        );
    }
}
