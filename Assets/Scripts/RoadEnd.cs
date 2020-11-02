using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEnd : MonoBehaviour
{
    public CarSpawner spawner;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.name.Contains("Car"))
            spawner.CarHitRoadEnd(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }
}
