using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopStart : MonoBehaviour
{
    public StopEnd end;
    public StopMiddle mid;

    private void OnTriggerStay(Collider other)
    {
        Car car = other.GetComponent<Car>();
        if (car == null) return;
        if (end.occupied && mid.occupied)
        {
            car.Stop();
        }
        else
        {
            car.Go();
        }
    }
}
