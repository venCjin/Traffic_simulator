﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarSpawner : MonoBehaviour
{
    public PathCreator Path;
    //public RoadEnd roadEnd;
    public GameObject carPrefab;
    public int CarNumber;
    //[SerializeField] private GameObject[] _carPool;
    //[SerializeField] private uint _traveledCount = 0;

    private IEnumerator Spawn()
    {
        for (int i = 0; i < CarNumber; i++)
        {
            Car car = Instantiate(carPrefab, transform).GetComponent<Car>();
            car.StartPath = Path;
            car.ActualPath = Path;
            car.A = Random.Range(0.5f, 0.5f);
            car.MaxSpeed = Random.Range(50.0f, 50.0f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    /*public void CarHitRoadEnd(GameObject car)
    {
        car.GetComponent<Car>().distanceTravelled = 0.0f;
        car.SetActive(false);
        _traveledCount += 1;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        //roadEnd = GetComponentInChildren<RoadEnd>();
        //if(roadEnd) roadEnd.spawner = this;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
