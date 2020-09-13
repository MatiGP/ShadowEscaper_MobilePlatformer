using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rocketSpeed;
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

        transform.Translate(transform.right * rocketSpeed * Time.deltaTime);

    }

    public void LaunchRocket()
    {
        launchRocket = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) Debug.Log("Rocket has hit the player!");
        OnDestroyRocket.Invoke();
        gameObject.SetActive(false);
    }
}
