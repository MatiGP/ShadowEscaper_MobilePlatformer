using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed;

    bool launchRocket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!launchRocket) return;
        


    }

    public void LaunchRocket()
    {
        launchRocket = true;
    }
}
