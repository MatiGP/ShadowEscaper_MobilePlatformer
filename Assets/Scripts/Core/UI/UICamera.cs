using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    [SerializeField] private float m_PanelWidth = 1920f;
    [SerializeField] private Camera m_Camera = null;

    private void Start()
    {
        float aspectRatio = (float)Screen.height / (float)Screen.width;

        m_Camera.orthographicSize = (m_PanelWidth * aspectRatio) / 2.0f; 
    }


}
