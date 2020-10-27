using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarController : MonoBehaviour
{
    //[SerializeField] private bool _canMove = true;
    [SerializeField] private Rigidbody _rigidbody;
    public float MaxSpeed = 10;
    public float A = .0001f;
    public float epsilon = .5f;
    public PathCreator Path;
    public float Speed = .0f;
    private float distanceTravelled = 0.0f;

    private Coroutine accCorutine;


    public IEnumerator Accelarate(float a, float destSpeed)
    {
        while(Mathf.Abs(Speed) < destSpeed)
        {
            Speed += a;
            yield return new WaitForEndOfFrame();
        }
        
    }

    public IEnumerator Deaccelarate(float a, float destSpeed)
    {
        while (Speed > destSpeed)
        {
            Speed -= a;
            yield return new WaitForEndOfFrame();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        accCorutine = StartCoroutine(Accelarate(A, MaxSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += Speed * Time.deltaTime;
        transform.position = Path.path.GetPointAtDistance(distanceTravelled);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("brake"))
        {
            //StopCoroutine(accCorutine);
            Debug.Log("braking");
            //accCorutine =  StartCoroutine(Deaccelarate(-A, 0));
        }
    }
}
