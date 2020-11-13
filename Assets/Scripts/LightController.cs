using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Transform _stopLine { get; private set; }
    private Material _currentMaterial;
    
    [SerializeField] public bool canGoThrought = false;
    public float redTime = 4;
    public float greenTime = 4;
    [SerializeField] private float _changeInterval; //seconds

    private float _timer = 0;

    public void Setup()
    {
        _currentMaterial = gameObject.GetComponent<MeshRenderer>().material;
        _currentMaterial.EnableKeyword("_EMISSION");
        if (canGoThrought)
        {
            //_currentMaterial.color = Color.green;
            _currentMaterial.SetColor("_EmissionColor", Color.green);
            _changeInterval = greenTime;
        }
        else
        {
            //_currentMaterial.color = Color.red;
            _currentMaterial.SetColor("_EmissionColor", Color.red);
            _changeInterval = redTime;
        }
        _stopLine = GetComponentInChildren<Transform>();
    }

    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        //Debug.Log(_canGoThrought);
        if(_timer > _changeInterval)
        {
            _timer = 0;
            canGoThrought = !canGoThrought;
            if (canGoThrought)
            {
                 //_currentMaterial.color = Color.green;
                _currentMaterial.SetColor("_EmissionColor", Color.green);
            }
            else
            {
                //_currentMaterial.color = Color.red;
                _currentMaterial.SetColor("_EmissionColor", Color.red);
            }
        }
    }
}
