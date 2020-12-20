using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnd : MonoBehaviour
{
    public bool occupied = false;

    private void OnTriggerStay(Collider other)
    {
        Car car = other.GetComponent<Car>();
        //if (car == null) return;
        //occupied = true;
        if(car && car.braking) { occupied = true; Debug.Log("braking"); }
        else { occupied = false; Debug.Log("go");  }

    }

}
