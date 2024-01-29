using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Awake()
    {
        if (player == null)
            player = GetComponent<PlayerMovement>().transform;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
