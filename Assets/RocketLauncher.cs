using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [Header("Rocket Manager")]
    [SerializeField] Rocket rocket;
    [SerializeField] float rocketLoadTime;
    Vector3 rocketStartPosition;

    [Header("Launcher Rotation")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] float rotationStopDuration;
    [Header("Warning")]
    [SerializeField] ExclamationMark mark;
    
    float currentAngleZ;
    bool rotateClockwise = true;
    bool rocketLaunched;
    bool stopRotating;

    private void Start()
    {
        rocketStartPosition = rocket.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopRotating) return;

        if (currentAngleZ >= maxRotation)
        {
            rotateClockwise = false;
        }
        else if(currentAngleZ <= minRotation)
        {
            rotateClockwise = true;
        }

        if (rotateClockwise)
        {
            currentAngleZ += rotateSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);                       
        }

        if (!rotateClockwise)
        {
            currentAngleZ -= rotateSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, -rotateSpeed * Time.deltaTime);
        }
        
    }

    public void ResetRocket()
    {
        StartCoroutine(LoadRocket());
    }

    public void Launch()
    {
        if (rocketLaunched) return;
        StartCoroutine(StopRotating());
        rocketLaunched = true;
        StartCoroutine(LaunchRocket());
    }

    IEnumerator StopRotating()
    {
        stopRotating = true;
        yield return new WaitForSeconds(rotationStopDuration+0.4f);
        stopRotating = false;
    }

    IEnumerator LaunchRocket()
    {
        mark.transform.localRotation = Quaternion.identity;
        mark.ShowMark();
        yield return new WaitForSeconds(rotationStopDuration);
        mark.HideMark();
        rocket.LaunchRocket();
    }

    IEnumerator LoadRocket()
    {       
        rocket.gameObject.SetActive(false);
        yield return new WaitForSeconds(rocketLoadTime);
        rocket.transform.SetParent(transform);
        rocket.transform.localRotation = Quaternion.Euler(0f, 0f, -30f);
        rocket.transform.localPosition = rocketStartPosition;
        rocketLaunched = false;
        rocket.gameObject.SetActive(true);
    }
}
