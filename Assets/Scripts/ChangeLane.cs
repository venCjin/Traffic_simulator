using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeLane : MonoBehaviour
{
    /// <summary>
    /// probablity of changing lane in %
    /// </summary>
    [Range(0,100)]
    public float probablity = 100.0f;
    public PathCreator newPath;
    public float newPathDistance = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        Car car = other.GetComponent<Car>();
        if (car == null) return;
        // random in probablity
        if (Random.Range(0.0f, 100.0f) < probablity)
        {
            // zmien
            car.ActualPath = newPath;
            car.distanceTravelled = newPathDistance;
            car.observedLight = null;
        } 
        // else { /*nie zmieniaj*/ }
    }
}
