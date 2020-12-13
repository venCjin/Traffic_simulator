using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMaterialManager : MonoBehaviour
{
    public Material material;
    [SerializeField] private Car car;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        car = GetComponentInParent<Car>();
    }

    // Update is called once per frame
    void Update()
    {
        material.color = Vector4.Lerp(Color.red, Color.green, car.Speed / car.MaxSpeed);
    }
}
