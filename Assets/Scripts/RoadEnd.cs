using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEnd : MonoBehaviour
{
    //public CarSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        //if (other.gameObject.name.Contains("Car"))
        //    spawner.CarHitRoadEnd(other.gameObject);
        Car car = other.GetComponent<Car>();
        if(car!=null)
        {
            car.ActualPath = car.StartPath;
            car.distanceTravelled = 0;
        }
    }
}
