using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //[SerializeField] private bool _canMove = true;
    [SerializeField] private Rigidbody _rigidbody;
    public float MaxSpeed = 1;
    public float A = .001f;
    public float epsilon = .5f;
    public Vector3 Direction = new Vector3(0.0f, 0.0f, 1.0f);
    private Coroutine accCorutine;
    public IEnumerator Accelarate(float a, float destSpeed)
    {
        while(_rigidbody.velocity.magnitude < destSpeed)
        {
            _rigidbody.velocity += Direction * a;
            Debug.Log(Direction * a);
            yield return new WaitForEndOfFrame();
        }
        
    }

    public IEnumerator Deaccelerate(float a, float destSpeed)
    {
        

        while (_rigidbody.velocity.magnitude > destSpeed)
        {
            Debug.Log(_rigidbody.velocity);
            _rigidbody.velocity -= Direction * a;
            if (_rigidbody.velocity.magnitude < epsilon) {
                _rigidbody.velocity = Vector3.zero;
                yield break;
            }
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

        //Debug.Log(_rigidbody.velocity);
        // _canMove = ? true : flase
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("brake"))
        {
            StopCoroutine(accCorutine);
            Debug.Log("braking");
            accCorutine =  StartCoroutine(Deaccelerate(.4f*A, 0));
        }
    }
}
