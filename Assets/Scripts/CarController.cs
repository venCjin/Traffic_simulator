using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarController : MonoBehaviour
{
    //[SerializeField] private bool _canMove = true;
    //

    [Header("DRIVER PARAMETERS")]
    public PathCreator Path;
    public float Speed = 20.0f;
    public float SeenDistance = 15;
    [Header("CAR PARAMETERS")]
    public float MaxSpeed = 1;
    public float A = .0001f;
    [Header("LIGHT")]
    [SerializeField] private LightController _observedLight = null;
    [Header("RAYCASTING")]
    //Things for raycasting
    public float CheckingFrequency = 1;
    public float Angle = 45;
    public float Step = 1;
    [Header("DEBUG")]
    [SerializeField] private float distanceTravelled = 0.0f;
    [SerializeField] private Coroutine accCorutine;
    [SerializeField] private bool _braking;


    public IEnumerator Accelarate(float a, float destSpeed)
    {
        while(Mathf.Abs(Speed) < destSpeed)
        {
            if (_braking) yield break;
            Speed += a;

            //Debug.Log("Accelarate");
            yield return new WaitForEndOfFrame();
        }
        
    }

    public IEnumerator Deaccelarate(float a, float destSpeed)
    {
        while (Speed > destSpeed)
        {
            Speed -= a;
            //Debug.Log("Deaccelarate");
            yield return new WaitForEndOfFrame();
        }
    }

    public void StopBeforePosition(Vector3 stop, float epsilon)
    {
        float distance;
        Vector3 v = (transform.position - stop);
        v.y = 0;
        distance = v.magnitude;
        if (distance > epsilon && Speed > 0)
        {
            Speed -= 2*(MaxSpeed*Time.deltaTime); //tzw wspolczynnik studenta
        }
        else
        {
            Speed = 0.0f;
            _braking = false;
        }
    }

    public IEnumerator CheckForObstacles()
    {
        while (true)
        {
            //Debug.Log(gameObject.name + " " + "Checking for obstacles...");
            
            for(Vector3 i =  Quaternion.Euler(0.0f, -Angle/2, 0.0f) * transform.forward;
                i != Quaternion.Euler(0.0f, Angle / 2, 0.0f) * transform.forward;
                i = Quaternion.Euler(0.0f, Step, 0.0f) * i)
            {
                for(Vector3 j = Quaternion.Euler(-Angle / 2, 0.0f, 0.0f)*i;
                    j != Quaternion.Euler(Angle / 2, 0.0f, 0.0f) * i;
                    j = Quaternion.Euler(Step, 0.0f, 0.0f) * j)
                {
                    Debug.DrawRay(transform.position, j * 10, Color.white, 1/CheckingFrequency);
                    //Look for lights
                    if(Physics.Raycast(transform.position, j, out RaycastHit hit, SeenDistance))
                    {
                        
                        GameObject HitObject = hit.collider.gameObject;
                        //Debug.Log("Seeng something... " + HitObject.name);
                        LightController light = HitObject.GetComponent<LightController>();
                        if (light != null)
                        {
                            Debug.DrawRay(transform.position, j * 30, Color.red, 1);
                            if(!light.canGoThrought) _observedLight = light;
                            
                        }
                    }
                }
                
            }
            yield return new WaitForSeconds(1 / CheckingFrequency);
        }
    }

    private void BrakeBeforeLight(LightController light)
    {
        //StopCoroutine(accCorutine);
        //accCorutine = StartCoroutine(StopBeforePosition(light._stopLine.position, 0.1f));

    }

    // Start is called before the first frame update
    void Start()
    {
        accCorutine = StartCoroutine(Accelarate(A, MaxSpeed));
        StartCoroutine(CheckForObstacles());
    }

    // Update is called once per frame
    void Update()
    {
        //moving
        
        distanceTravelled += Speed * Time.deltaTime;
        transform.position = Path.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = Path.path.GetRotationAtDistance(distanceTravelled);
        
        //observe light
        if(_observedLight != null)
        {
            if(_observedLight.canGoThrought)
            {
                _observedLight = null;
                _braking = false;
                //StopCoroutine(accCorutine);
                accCorutine = StartCoroutine(Accelarate(A, MaxSpeed));
            }
            else
            {
                StopBeforePosition(_observedLight.transform.position, 0.0f);
                _braking = true;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("brake"))
        {
            //StopCoroutine(accCorutine);
            //Debug.Log("braking");
            //accCorutine =  StartCoroutine(Deaccelarate(A, 0));
        }
    }
}
