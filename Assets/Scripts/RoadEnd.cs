using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEnd : MonoBehaviour
{
    public CarSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        if (other.gameObject.name.Contains("Car"))
            spawner.CarHitRoadEnd(other.gameObject);
    }
}
