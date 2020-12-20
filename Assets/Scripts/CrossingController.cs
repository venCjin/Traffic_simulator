using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage
{
    public Stage(float d, List<bool> list)
    {
        duration = d;
        isGreen = list;
    }
    public float duration;
    public List<bool> isGreen;
}
public class CrossingController : MonoBehaviour
{
    public bool MostCars = true; 
    public List<LightController> lights;
    [HideInInspector] public float timer = 0;
    public int stageIndex = 0;
    public List<Stage> stages;
    public List<CarCounter> counters;

    void Start()
    {
        setLights();
    }

    void Update()
    {
        float duration;
        if (MostCars) duration = 2.0f;
        else duration = stages[stageIndex].duration;

        timer += Time.deltaTime;
        if (timer > duration)
        {
            timer = 0;
            
            if (MostCars)
            {
                mostCars();
            }
            else
            {
                // fixed stage and time
                stageIndex++;
                if (stageIndex > stages.Count - 1) stageIndex = 0;            
            }

            setLights();
        }
    }

    private void setLights()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].canGoThrought = stages[stageIndex].isGreen[i];
        }
    }

    private void mostCars()
    {
        int most = 0;
        for (int i = 0; i < stages.Count; i++)
        {
            int stageMost = 0;
            int j = 0;
            for (; j < stages[i].isGreen.Count; j++)
            {
                if (stages[i].isGreen[j]) stageMost += counters[j].Count;
            }
            stageMost /= j;
            if (stageMost > most) { most = stageMost; stageIndex = i; }
        }
    }
}
