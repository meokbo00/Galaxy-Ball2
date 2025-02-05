using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    private GameObject player;
    private GameObject mainPlayer;
    public Camera mainCamera;

    private Vector3 offsetMainPlayer = new Vector3(0, 1.1f, 0);
    private Vector3 offsetPlayer = new Vector3(0, 5, 0);

    void Awake()
    {
        player = GameObject.Find("Player");
        mainPlayer = GameObject.Find("Main Player");
    }

    void LateUpdate()
    {
        Vector3 targetPos;

        if (mainPlayer != null)
        {
            targetPos = mainPlayer.transform.position + offsetMainPlayer;
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
        else if (player != null)
        {
            targetPos = player.transform.position + offsetPlayer;
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
        // No else needed; simply return if both players are null.
    }
}
