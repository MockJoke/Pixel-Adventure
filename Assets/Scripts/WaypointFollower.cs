using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Transform[] waypoints;
    protected int currWaypointIndex = 0;
    public float closingDistance = 0.1f;

    public float speed = 2f;

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
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
