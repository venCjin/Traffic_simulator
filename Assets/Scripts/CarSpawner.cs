using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarSpawner : MonoBehaviour
{
    public PathCreator Path;
    public GameObject carPrefab;

    private IEnumerator Spawn()
    {
        for (int i = 0; i < 5; i++)
        {
            Car car = Instantiate(carPrefab, transform).GetComponent<Car>();
            car.Path = Path;
            car.A = Random.Range(0.03f, 0.2f);
            car.MaxSpeed = Random.Range(10.0f, 30.0f);
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
