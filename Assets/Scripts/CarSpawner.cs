using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarSpawner : MonoBehaviour
{
    public PathCreator Path;
    public GameObject carPrefab;
    public float angleX = 0.0f;
    public float angleY = 0.0f;
    public float angleZ = 0.0f;

    private IEnumerator Spawn()
    {
        Car prev = null;
        for (int i = 0; i < 5; i++)
        {
            Car car = Instantiate(carPrefab, transform).GetComponent<Car>();
            car.Path = Path;
            car.angleX = angleX;
            car.angleY = angleY;
            car.angleZ = angleZ;
            car.A = Random.Range(0.03f, 0.2f);
            car.MaxSpeed = Random.Range(20.0f, 50.0f);
            car.carInFront = prev;
            prev = car;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
