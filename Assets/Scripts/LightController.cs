using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] public bool _canGoThrought { get; private set; } = false;
    public float ChangeInterval = 1; //seconds

    public Transform _stopLine { get; private set; }
    private Material _currentMaterial;
    private float _timer;

    void Start()
    {
        _currentMaterial = gameObject.GetComponent<MeshRenderer>().material;
        _currentMaterial.color = Color.green;
        _canGoThrought = true;
        _stopLine = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        //Debug.Log(_canGoThrought);
        if(_timer > ChangeInterval)
        {
            _timer = 0;
            _canGoThrought = !_canGoThrought;
            if (_canGoThrought)
            {
                 _currentMaterial.color = Color.green;
            }
            else
            {
                _currentMaterial.color = Color.red;
            }
        }
    }
}
