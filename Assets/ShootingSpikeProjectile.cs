using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingSpikeProjectile : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] UnityEvent OnCollided;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
