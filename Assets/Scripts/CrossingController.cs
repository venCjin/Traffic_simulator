using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage
{
    public Stage(float d, List<bool> list)
    {
        //base();
        duration = d;
        isGreen = list;
    }
    public float duration;
    public List<bool> isGreen;
}
public class CrossingController : MonoBehaviour
{
    public List<LightController> lights;
    [HideInInspector] public float timer = 0;
    public int stageIndex = 0;
    public List<Stage> stages;
    

    void Start()
    {
        setLights();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > stages[stageIndex].duration)
        {
            timer = 0;
            stageIndex++;
            if (stageIndex > stages.Count - 1) stageIndex = 0;

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
}
