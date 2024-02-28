using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float closingDistance = 0.1f;
    private int currWaypointIndex = 0;

    public float speed = 2f;

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(waypoints[currWaypointIndex].position, transform.position) < closingDistance)
        {
            currWaypointIndex++;
            
            if (currWaypointIndex >= waypoints.Length)
            {
                currWaypointIndex = 0;
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currWaypointIndex].position, Time.deltaTime * speed);
    }
}
