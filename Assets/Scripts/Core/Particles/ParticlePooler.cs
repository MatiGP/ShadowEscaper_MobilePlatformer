using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : MonoBehaviour
{
    [SerializeField] private List<ParticleItem> m_ParticlePrefabs = null;
    
    [SerializeField] private Transform m_PoolRoot = null;

    private Dictionary<EParticleType, Queue<ParticleItem>> m_ParticlePools = null;

    private void Awake()
    {
        m_ParticlePools = new Dictionary<EParticleType, Queue<ParticleItem>>();

        for (int i = 0; i < m_ParticlePrefabs.Count; i++)
        {
            Queue<ParticleItem> particleItems = new Queue<ParticleItem>();

            for (int j = 0; j < m_ParticlePrefabs[i].MaxParticleCount; j++)
            {
                ParticleItem particleItem = Instantiate(m_ParticlePrefabs[i], m_PoolRoot);
                particleItem.gameObject.SetActive(false);

                particleItem.OnParticleLifetimeEnded += HandleParticleLifetimeEnded;

                particleItems.Enqueue(particleItem);
            }

            m_ParticlePools.Add(m_ParticlePrefabs[i].ParticleType, particleItems);
        }
    }

    private void HandleParticleLifetimeEnded(object sender, ParticleItem particle)
    {
        m_ParticlePools[particle.ParticleType].Enqueue(particle);
        particle.transform.SetParent(m_PoolRoot);
        particle.transform.position = Vector3.zero;
    }

    public void SpawnParticle(EParticleType type, Vector3 position, float zRot = 0f)
    {
        Queue<ParticleItem> particleItems = m_ParticlePools[type];

        if(particleItems.Count > 0)
        {
            ParticleItem particleItem = particleItems.Dequeue();
            particleItem.transform.SetParent(null);
            particleItem.transform.position = position;
            particleItem.transform.rotation = Quaternion.Euler(0, 0, zRot);
            particleItem.gameObject.SetActive(true);
        }
    }

}

public enum EParticleType
{
    JumpShockwave,
    WallJumpDust
}
