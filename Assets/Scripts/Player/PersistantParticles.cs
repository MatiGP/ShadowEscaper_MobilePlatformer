using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_WallSlideParticleObject = null;
    [SerializeField] private ParticleSystem m_GroundSlideParticleObject = null;

    private Dictionary<EMovementStateType, ParticleSystem> m_MovementParticles;

    private void Awake()
    {
        m_MovementParticles = new Dictionary<EMovementStateType, ParticleSystem>() { 
            { EMovementStateType.Groundsliding, m_GroundSlideParticleObject },
            { EMovementStateType.Wallsliding, m_WallSlideParticleObject }
        };      
    }

    public void SetEnabledParticleForMovementState(EMovementStateType movementStateType, bool enabled)
    {
        if (enabled)
        {
            m_MovementParticles[movementStateType].Play();
        }
        else
        {
            m_MovementParticles[movementStateType].Stop();
        }
    }   
}
