using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMiddle : MonoBehaviour
{
    public bool occupied = false;

    private void OnTriggerStay(Collider other)
    {
        //Car car = other.GetComponent<Car>();
        //if (car == null) return;
        occupied = true;

    }

    private void OnTriggerExit(Collider other)
    {
        occupied = false;
    }
}
