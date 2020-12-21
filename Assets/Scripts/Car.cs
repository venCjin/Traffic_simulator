using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Car : MonoBehaviour
{
    [Header("DRIVER PARAMETERS")]
    public PathCreator StartPath;
    public PathCreator ActualPath;
    public float Speed = 20.0f;
    public float SeenDistance = 1.0f;

    [Header("CAR PARAMETERS")]
    public float carWidth = 10.0f;
    public float MaxSpeed = 20.0f;
    public float A = 0.1f;
    public bool accelerating = true;
    public bool braking;

    [Header("OBSTACLES")]
    public LightController observedLight = null;
    public Car inFront = null;

    [Header("DEBUG")]
    public float distanceTravelled = 0.0f;

    void Start()
    {
        StartCoroutine(Accelarate(A, MaxSpeed));
        StartCoroutine(CheckForLights());
    }

    private void OnMouseDown()
    {
        Camera.main.GetComponent<CameraController>().carToFollow = gameObject;
    }

    void Update()
    {
        //moving
        distanceTravelled += Speed * Time.deltaTime;
        transform.position = ActualPath.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = ActualPath.path.GetRotationAtDistance(distanceTravelled);

        // stop after car
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, SeenDistance))
        {
            inFront = hit.collider.gameObject.GetComponent<Car>();
            if (inFront)
            {
                if (Vector3.Distance(transform.position, inFront.transform.position) < carWidth)
                {
                    if (Speed > inFront.Speed) Speed = 0.9f * inFront.Speed; // braking and keeping distance
                    if (inFront.braking) { Stop(); }
                    else { Go(); }
                }
            }
        }

        if (observedLight)
        {
            //green light
            if (observedLight.canGoThrought)
            {
                observedLight = null;
                Go();
            }
            //red light
            else
            {
                Stop();
            }
        }


        if (inFront != null && (Vector3.Dot(inFront.transform.forward, transform.forward) < 0.0f || Vector3.Distance(inFront.transform.position, transform.position) > SeenDistance))
        {
            inFront = null;
        }

        if (inFront == null && observedLight == null) braking = false;
    }

    public void Go()
    {
        braking = false;
        if (!accelerating && Speed < MaxSpeed) StartCoroutine(Accelarate(A, MaxSpeed));
    }

    public void Stop()
    {
        braking = true;
        Speed = 0;
    }

    public IEnumerator Accelarate(float a, float destSpeed)
    {
        accelerating = true;
        while (Mathf.Abs(Speed) < destSpeed)
        {
            if (braking)
            {
                accelerating = false;
                yield break;
            }
            Speed += a;
            yield return new WaitForEndOfFrame();
        }
        accelerating = false;
    }



    public IEnumerator CheckForLights()
    {
        while (true)
        {
            Vector3 v = transform.up + transform.forward;

            if (Physics.Raycast(transform.position, v, out RaycastHit hit, SeenDistance))
            {
                if(Vector3.Dot(hit.collider.gameObject.transform.forward, transform.forward) < 0.0f)
                {
                    LightController light = hit.collider.gameObject.GetComponent<LightController>();
                    if (light)
                    {
                        observedLight = light;
                    }
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

}