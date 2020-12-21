using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrafficSlider : MonoBehaviour
{
    private Slider slider;
    public List<CarSpawner> spawners;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        //UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TrafficChange()
    {
        foreach(var spawner in spawners)
        {
            spawner.CarNumber = (int)slider.value * spawner.initialCarNumber;
            spawner.StartSpawn();
        }
    }
}
