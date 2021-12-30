using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera m_PlayerCamera = null;
    [SerializeField] private CinemachineVirtualCamera m_TutorialCharacterCamera = null;
    [SerializeField] private float m_DefaultZoom = 13f;
    [SerializeField] private float m_MinimumZoom = 5f;

    [SerializeField] private AnimationCurve m_ZoomAnimCurve = null;

    private float m_ZoomFactor = 0;

    private void Start()
    {
        m_ZoomFactor = m_DefaultZoom - m_MinimumZoom;
    }

    public void ZoomOut()
    {
        StartCoroutine(StartZoomOut());
    }

    private IEnumerator StartZoomOut()
    {
        float timePassed = 0f;
        while (m_PlayerCamera.m_Lens.OrthographicSize < m_DefaultZoom)
        {
            timePassed += Time.deltaTime;
            m_PlayerCamera.m_Lens.OrthographicSize = m_MinimumZoom + (m_ZoomFactor * m_ZoomAnimCurve.Evaluate(timePassed));      
            yield return null;
        }
    }

    public void ZoomIn()
    {
        StartCoroutine(StartZoomIn());
    }

    private IEnumerator StartZoomIn()
    {
        float timePassed = 0f;
        while (m_PlayerCamera.m_Lens.OrthographicSize > m_MinimumZoom)
        {
            timePassed += Time.deltaTime;
            m_PlayerCamera.m_Lens.OrthographicSize = m_DefaultZoom - (m_ZoomFactor * m_ZoomAnimCurve.Evaluate(timePassed));
            Debug.Log("Otho size" + m_PlayerCamera.m_Lens.OrthographicSize);
            yield return null;
        }
    }

    public void ChangeViewToTutorialCharacter()
    {
        m_PlayerCamera.m_Priority = 0;
        m_TutorialCharacterCamera.m_Priority = 10;
    }

    public void ChangeViewToPlayerCharacter()
    {
        m_PlayerCamera.m_Priority = 10;
        m_TutorialCharacterCamera.m_Priority = 0;
    }
}
