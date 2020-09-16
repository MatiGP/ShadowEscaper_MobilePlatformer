using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpikes : MonoBehaviour
{
    [SerializeField] Transform spikes;

    public void ResetSpikes()
    {
        spikes.position = new Vector3(0, 0.8f);
        spikes.gameObject.SetActive(true);
    }
}
