using Cinemachine;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private Transform player;

    void Awake()
    {
        if (player == null)
            player = GetComponent<PlayerMovement>().transform;

        vCam.Follow = player;
    }
}
