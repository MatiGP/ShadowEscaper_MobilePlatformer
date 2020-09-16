using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpikes : MonoBehaviour
{
    [SerializeField] GameObject spikes;

    public void ResetSpikes()
    {
        spikes.SetActive(false);
        spikes.transform.localPosition = new Vector2(0, 0.8f);        
        spikes.SetActive(true);
    }


}
