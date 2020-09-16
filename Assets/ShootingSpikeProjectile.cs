using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingSpikeProjectile : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float lifetime;
    [SerializeField] UnityEvent OnCollided;

    float time;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f);
        time += Time.deltaTime;
        if(time >= lifetime)
        {
            time = 0f;
            OnCollided.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollided.Invoke();       
    }

    
}
