using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBackground : MonoBehaviour
{
    [SerializeField] private Vector2 m_BackgroundMoveVector = new Vector2(24, 12);

    [SerializeField] private float m_HorizontalThreshold = 20f;
    [SerializeField] private float m_VerticalThreshold = 10f;

    [SerializeField] private Transform m_PlayerTransform = null;

    private Vector3 distance = Vector3.zero;

    private void Awake()
    {
        transform.position = m_PlayerTransform.position;
    }

    private void Update()
    {
        distance = m_PlayerTransform.position - transform.position;

        HandleMovingLeftRight();
        HandleMovingUpDown();
        
    }

    private void HandleMovingLeftRight()
    {
        if(distance.x > m_HorizontalThreshold)
        {
            transform.position += Vector3.right * m_BackgroundMoveVector.x;
        }
        else if(distance.x < -m_HorizontalThreshold)
        {
            transform.position += Vector3.left * m_BackgroundMoveVector.x;
        }
    }

    private void HandleMovingUpDown()
    {
        if (distance.y > m_VerticalThreshold)
        {
            transform.position += Vector3.up * m_BackgroundMoveVector.y;
        }
        else if (distance.y < -m_VerticalThreshold)
        {
            transform.position += Vector3.down * m_BackgroundMoveVector.y;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * m_HorizontalThreshold);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_VerticalThreshold);
    }
}
