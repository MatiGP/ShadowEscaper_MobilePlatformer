using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] float spikeActivationTime;
    [SerializeField] float spikeStayTime;
    [SerializeField] Animator spikeAnimator;
    [SerializeField] BoxCollider2D spikeCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ActivateSpikes());
    }

    IEnumerator ActivateSpikes()
    {
        yield return new WaitForSeconds(spikeActivationTime);
        spikeAnimator.SetTrigger("setSpikes");
        spikeAnimator.ResetTrigger("hideSpikes");
        spikeCollider.enabled = true;
        yield return new WaitForSeconds(spikeStayTime);
        spikeAnimator.SetTrigger("hideSpikes");
        spikeAnimator.ResetTrigger("setSpikes");
        spikeCollider.enabled = false;
    }
}
