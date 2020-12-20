using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarSpawner : MonoBehaviour
{
    public PathCreator Path;
    //public RoadEnd roadEnd;
    public GameObject carPrefab;
    public int CarNumber;
    public List<Car> cars;
    public int initialCarNumber;

    Coroutine SpawnCorutine;
    //[SerializeField] private GameObject[] _carPool;
    //[SerializeField] private uint _traveledCount = 0;

    private IEnumerator Spawn()
    {
        if (CarNumber - cars.Count > 0)
        {
            while(cars.Count < CarNumber)
            {
                Car car = Instantiate(carPrefab, transform).GetComponent<Car>();
                car.StartPath = Path;
                car.ActualPath = Path;
                car.A = Random.Range(0.4f, 0.9f);
                car.MaxSpeed = Random.Range(30.0f, 60.0f);
                cars.Add(car);
                yield return new WaitForSeconds(0.3f);
            }
        }
        else
        {
            while(cars.Count > CarNumber)
            {
                Destroy(cars[0]);
                cars.RemoveAt(0);
            }
        }
    }

    /*public void CarHitRoadEnd(GameObject car)
    {
        car.GetComponent<Car>().distanceTravelled = 0.0f;
        car.SetActive(false);
        _traveledCount += 1;
    }*/

    // Start is called before the first frame update
    
    public void StartSpawn()
    {
        Debug.Log(SpawnCorutine);
        if(SpawnCorutine != null)
        {
            StopCoroutine(SpawnCorutine);
        }
        SpawnCorutine = StartCoroutine(Spawn());
    }
    void Start()
    {
        cars = new List<Car>();
        //roadEnd = GetComponentInChildren<RoadEnd>();
        //if(roadEnd) roadEnd.spawner = this;
        initialCarNumber = CarNumber;
        StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
