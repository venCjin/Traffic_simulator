using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Car : MonoBehaviour
{
    [Header("DRIVER PARAMETERS")]
    public PathCreator Path;
    public float Speed = 20.0f;
    public float SeenDistance = 6.0f;

    [Header("CAR PARAMETERS")]
    public float MaxSpeed = 20.0f;
    public float A = 0.1f;
    [SerializeField] private bool _braking;

    [Header("LIGHT")]
    [SerializeField] private LightController _observedLight = null;
    //[SerializeField] private float _startBrakingDistance = 0.0f;


    //[Header("RAYCASTING")]
    //Things for raycasting
    //public float CheckingFrequency = 1;
    //public float Step = 5;
    //public float distanceToObstacle = 0; //starting dis to light

    [Header("DEBUG")]
    public float distanceTravelled = 0.0f;
    public float carWidth = 5.0f;
    public bool accelerating = true;

    void Start()
    {
        StartCoroutine(Accelarate(A, MaxSpeed));
        StartCoroutine(CheckForLights());
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
    }

    private void OnMouseDown()
    {
        Camera.main.GetComponent<CameraController>().carToFollow = gameObject;
    }

    void Update()
    {
        //moving
        distanceTravelled += Speed * Time.deltaTime;
        transform.position = Path.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = Path.path.GetRotationAtDistance(distanceTravelled);

        // stop after car
        Debug.DrawRay(transform.position, transform.forward * carWidth, Color.white, 0.33f);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, SeenDistance))
        {
            Car inFront = hit.collider.gameObject.GetComponent<Car>();
            if (inFront)
            {
                Debug.DrawRay(transform.position, transform.forward * carWidth, Color.red, 0.33f);
                if (Vector3.Distance(transform.position, inFront.transform.position) < carWidth)
                {
                    if (Speed > inFront.Speed) Speed = 0.9f * inFront.Speed; // hamowanie i utrzymywanie odstępu
                    if (inFront._braking) { _braking = true; Speed = 0; }
                    else 
                    { 
                        _braking = false; 
                        
                        if(!accelerating && Speed < MaxSpeed) StartCoroutine(Accelarate(A, MaxSpeed));
                    }
                }
            }
        }

        if (_observedLight)
        {
            //green light
            if (_observedLight.canGoThrought)
            {
                _observedLight = null;
                _braking = false;
                if (!accelerating && Speed < MaxSpeed) StartCoroutine(Accelarate(A, MaxSpeed));
            }
            //red light
            else
            {
                _braking = true;
                Speed = 0;
                //StartCoroutine(Accelarate(-4.0f * A, 0.0f));
            }
        }

        

        #region old
        /*Vector3 i;
        for (float angle = 0; angle <= Angle; angle += Step)
        {
            i = Quaternion.Euler(0.0f, 0.0f, angle) * transform.forward;
            Debug.DrawRay(transform.position, i * 20, Color.white, 1 / 3);
                
            //Look for lights
            if (Physics.Raycast(transform.position, i, out RaycastHit hit, SeenDistance))
            {
                GameObject HitObject = hit.collider.gameObject;
                //Debug.Log("Seeng something... " + HitObject.name);
                LightController light = HitObject.GetComponent<LightController>();
                if (light != null)
                {
                    Debug.DrawRay(transform.position, i * 10, Color.red, 1);
                    if (!light._canGoThrought && _observedLight == null)
                    {
                        _observedLight = light;
                        _startBrakingDistance = Vector3.Distance(transform.position, light._stopLine.position);
                    }
                    else if (_observedLight != null)
                    {
                        // stop car at line
                        //Vector3 stop = new Vector3(light._stopLine.position.x, 0.0f, light._stopLine.position.z);
                        //Vector3 car = new Vector3(transform.position.x, 0.0f, transform.position.z);
                        //float distance = Vector3.Distance(car, stop);
                        //if (distance < 0.2f) distance = 0.0f;
                        float distance = Vector3.Distance(transform.position, light._stopLine.position);

                        Debug.Log("DISTANCE = " + distance + ";    start = " + _startBrakingDistance);
                        Speed = Mathf.Lerp(0.0f, Speed, distance/_startBrakingDistance);
                    }
                    else
                    {
                        Speed = MaxSpeed;
                    }
                }
            }
        }*/
        #endregion
    }


    public IEnumerator Accelarate(float a, float destSpeed)
    {
        accelerating = true;
        while (Mathf.Abs(Speed) < destSpeed)
        {
            if (_braking)
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
            Debug.DrawRay(transform.position, v * 10, Color.white, 0.33f);
            if (Physics.Raycast(transform.position, v, out RaycastHit hit, SeenDistance))
            {
                LightController light = hit.collider.gameObject.GetComponent<LightController>();
                if (light)
                {
                    Debug.DrawRay(transform.position, v * 10, Color.red, 0.33f);

                    //ustaw swiatlo
                    _observedLight = light;

                    //ustaw start hamowania
                    //_startBrakingDistance = Vector3.Distance(transform.position, light._stopLine.position);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }


/*
    public IEnumerator CheckForObstacles()
    {
        while (true)
        {
            // car in front
            Vector3 v = transform.forward;// + new Vector3(0, 0.1f, 0);
            if (Physics.Raycast(transform.position, v, out RaycastHit hit, SeenDistance))
            {
                Debug.DrawRay(transform.position, v * 10, Color.white, 1 / 3);
                Car other = hit.collider.gameObject.GetComponent<Car>();
                if (other)
                {
                    Debug.DrawRay(transform.position, v * 10, Color.red, 1 / 3);

                    if (other._stoped)
                    {
                        // stop after him
                    }
                }
            }
        }

    }
*/

}