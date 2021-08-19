using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed = 5;
    [SerializeField] UnityEvent OnDestroyRocket;

    bool launchRocket;

   
    // Update is called once per frame
    void Update()
    {
        if (!launchRocket) return;

        transform.Translate(Vector2.right * rocketSpeed * Time.deltaTime, Space.Self);
    }

    public void LaunchRocket()
    {
        transform.parent = null;
        launchRocket = true;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        launchRocket = false;
        ShadowRunApp.Instance.SoundManager.PlaySoundEffect(SoundType.RocketLauncher_Destroy);
        OnDestroyRocket.Invoke();    
    }
}
