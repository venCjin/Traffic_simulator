using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    //public Transform _stopLine { get; private set; }
    public Material material;
    
    [SerializeField] public bool canGoThrought = false;

    public void Setup()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
        material.EnableKeyword("_EMISSION");
        if (canGoThrought)
        {
            material.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            material.SetColor("_EmissionColor", Color.red);
        }
    }

    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (canGoThrought)
        {
            material.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            material.SetColor("_EmissionColor", Color.red);
        }
    }
}
