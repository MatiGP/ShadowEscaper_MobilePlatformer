using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] private bool m_ReverseSawDirection = false;
    [SerializeField] private float m_Speed = 4f;
    [SerializeField] private List<Vector2> m_SawWaypoints = new List<Vector2>();

    private int m_CurrentWaypointIndex;
    private int m_EndPathIndex = 0;

    void Start()
    {
        m_EndPathIndex = m_ReverseSawDirection ? 0 : m_SawWaypoints.Count - 1;

        if (m_ReverseSawDirection)
        {
            transform.position = m_SawWaypoints[m_SawWaypoints.Count - 1];
        }
        else
        {
            transform.position = m_SawWaypoints[0];
        }       
    }

    void Update()
    {
        if (ReachedEndOfPath())
        {
            m_CurrentWaypointIndex = m_ReverseSawDirection ? m_SawWaypoints.Count - 1 : 0;
        }

        if (IsSawAtPoint(m_SawWaypoints[m_CurrentWaypointIndex]))
        {
            m_CurrentWaypointIndex = m_ReverseSawDirection ? m_CurrentWaypointIndex++ : m_CurrentWaypointIndex--;
        }
        else
        {
            MoveToTheNextWaypoint();
        }
    }

    private void MoveToTheNextWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position
            ,m_SawWaypoints[m_CurrentWaypointIndex]
            ,m_Speed * Time.deltaTime);
    }
    private bool IsSawAtPoint(Vector2 position)
    {
        return transform.position == (Vector3)position;
    }
    private bool ReachedEndOfPath()
    {      
        return m_CurrentWaypointIndex == m_EndPathIndex;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < m_SawWaypoints.Count-1; i++)
        {
            Gizmos.DrawLine(m_SawWaypoints[i], m_SawWaypoints[i + 1]);
            if(i == m_SawWaypoints.Count - 2)
            {
                Gizmos.DrawLine(m_SawWaypoints[0], m_SawWaypoints[i+1]);
            }
        }
    }
}
