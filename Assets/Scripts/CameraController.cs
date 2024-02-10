using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float minYBound = -3.75f;
    [SerializeField] private float maxYBound = 5.75f;

    void Awake()
    {
        if (player == null)
            player = GetComponent<PlayerMovement>().transform;
    }

    void Update()
    {
        var playerPos = player.position;

        playerPos.y = Mathf.Clamp(playerPos.y, minYBound, maxYBound);
        
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }
}
