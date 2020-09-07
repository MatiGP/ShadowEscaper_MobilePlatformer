using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] bool stationary;
    [SerializeField] float speed;
    [SerializeField] List<Vector2> waypoints;

    int currentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (stationary) return;

        transform.position = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (stationary) return;

        if (ReachedEndOfPath()) currentWaypointIndex = 0; 

        if (IsSawAtPoint(waypoints[currentWaypointIndex]))
        {
            currentWaypointIndex++;
        }
        else
        {
            MoveToTheNextWaypoint(currentWaypointIndex);
        }
    }

    void MoveToTheNextWaypoint(int index)
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex], speed * Time.deltaTime);
    }

    bool IsSawAtPoint(Vector2 position)
    {
        return transform.position == (Vector3)position;
    }

    bool ReachedEndOfPath()
    {
        return currentWaypointIndex == waypoints.Count;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < waypoints.Count-1; i++)
        {
            Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
            if(i == waypoints.Count - 2)
            {
                Gizmos.DrawLine(waypoints[0], waypoints[i+1]);
            }
        }
    }
}
