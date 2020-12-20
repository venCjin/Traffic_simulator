using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCounter : MonoBehaviour
{
    [SerializeField] private int count = 0;
    [SerializeField] private int lanes = 1;
    public int Count => count / lanes;
    private void OnTriggerEnter(Collider other)
    {
        //Car car = other.GetComponent<Car>();
        //if (car == null) return;
        count++;
    }

    private void OnTriggerExit(Collider other)
    {
        //Car car = other.GetComponent<Car>();
        //if (car == null) return;
        count--;
    }
}
