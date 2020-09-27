using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed = 5;
    [SerializeField] UnityEvent OnDestroyRocket;

    bool launchRocket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        OnDestroyRocket.Invoke();    
    }
}
