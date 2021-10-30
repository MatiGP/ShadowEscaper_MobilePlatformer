using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketLauncher : MonoBehaviour
{
    [Header("Rocket Manager")]
    [SerializeField] private Rocket m_Rocket;
    [SerializeField] private float m_RockedLoadTime;
    private Vector3 rocketStartPosition;

    [Header("Launcher Rotation")]
    [SerializeField] private Vector3 m_MinRotation;
    [SerializeField] private Vector3 m_MaxRotation;
    [SerializeField] private float m_RotationDuration;
    [Header("Warning")]
    [SerializeField] ExclamationMark mark;   
    
    float currentAngleZ;
    bool rotateClockwise = true;
    bool rocketLaunched;
    bool stopRotating;

    private WaitForSeconds RotationStopDuration;
    private WaitForSeconds ExclamationMarkShowDuration;
    private WaitForSeconds RocketLoadTime;
    private Sequence m_RocketLauncherSequence;

    private void Awake()
    {
        m_RocketLauncherSequence = DOTween.Sequence();

        RotationStopDuration = new WaitForSeconds(m_RotationDuration);
        ExclamationMarkShowDuration = new WaitForSeconds(m_RotationDuration);
        RocketLoadTime = new WaitForSeconds(m_RockedLoadTime);

        m_RocketLauncherSequence
            .Append(transform.DOLocalRotate(m_MaxRotation, m_RotationDuration).SetDelay(1f))          
            .Append(transform.DOLocalRotate(Vector3.zero, m_RotationDuration))          
            .Append(transform.DOLocalRotate(m_MinRotation, m_RotationDuration).SetDelay(1f))
            .SetLoops(-1, LoopType.Yoyo);

        m_RocketLauncherSequence.Play();
    }


}
