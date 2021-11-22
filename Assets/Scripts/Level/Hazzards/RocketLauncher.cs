using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketLauncher : MonoBehaviour
{
    [Header("Rocket Manager")]
    [SerializeField] private Rocket m_Rocket = null;
    [SerializeField] private float m_RockedLoadTime = 1f;
    [SerializeField] private Vector3 m_RocketSpawnPosition = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 m_RocketReadyPosition = new Vector3(0, 0, 0);
    
    [Header("Rocket Launcher Laser")]
    [SerializeField] private RocketLauncherLaser m_RocketLauncherLaser = null;
    
    [Header("Launcher Rotation")]
    [SerializeField] private Vector3 m_MinRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 m_MaxRotation = new Vector3(0, 0, 0);
    [SerializeField] private float m_RotationDuration = 1f;
    
    [Header("Warning")]
    [SerializeField] ExclamationMark m_ExclamationMark;
    
    private Tween m_RotateDown;
    private Tween m_RotateUp;
    private Tween m_RocketLoad;

    private Sequence m_RotationSequence;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(m_MaxRotation);

        SetUpRotatingSequence();
        m_RotationSequence.Play();

        m_RocketLauncherLaser.OnPlayerDetected += HandlePlayerDetected;
        m_Rocket.OnRockedDestroyed += HandleRocketDestroyed;

        SetUpRocketTween();
    }


    private void HandleRocketDestroyed(object sender, System.EventArgs e)
    {
        PlayRotatingSequence();
        m_Rocket.transform.SetParent(transform);
        m_Rocket.transform.localPosition = m_RocketSpawnPosition;
        SetUpRocketTween();
        m_RocketLoad.Play().OnComplete(HandleRocketLoaded);
    }

    private void HandlePlayerDetected(object sender, System.EventArgs e)
    {
        ShadowRunApp.Instance.SoundManager.PlaySoundEffect(ESoundType.ROCKETLAUNCHER_PLAYERDETECTED);
        PauseRotatingSequence();        
        m_Rocket.LaunchRocket();      
    }

    private void HandleRocketLoaded()
    {
        m_RocketLauncherLaser.EnableLaser();
    }

    private void SetUpRotatingSequence()
    {
        m_RotationSequence = DOTween.Sequence();
        m_RotateDown = transform.DOLocalRotateQuaternion(Quaternion.Euler(m_MinRotation), 
            m_RotationDuration).SetEase(Ease.Linear);
        m_RotateUp = transform.DOLocalRotateQuaternion(Quaternion.Euler(m_MaxRotation), 
            m_RotationDuration).SetEase(Ease.Linear);

        m_RotationSequence.Append(m_RotateDown).Append(m_RotateUp).SetLoops(-1);
    }

    private void PauseRotatingSequence()
    {
        m_RotationSequence.Pause();
    }

    private void PlayRotatingSequence()
    {
        m_RotationSequence.Play();
    }

    private void SetUpRocketTween()
    {
        m_RocketLoad = m_Rocket.transform.DOLocalMove(m_RocketReadyPosition, m_RockedLoadTime);
    }

    private void OnDestroy()
    {
        m_RocketLauncherLaser.OnPlayerDetected -= HandlePlayerDetected;
    }


}
