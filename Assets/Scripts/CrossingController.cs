using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossingController : MonoBehaviour
{
    public List<LightController> lights;
    public int[,] red_green_canGo =
        {
            { 4, 4, 0 },
            { 4, 4, 1 }
        };
    

    void Start()
    {
        // set start lights values
        /*foreach (LightController light in lights)
        {
            ble[0,0] = 0;
        }*/
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].redTime = red_green_canGo[i, 0];
            lights[i].greenTime = red_green_canGo[i, 1];
            lights[i].canGoThrought = red_green_canGo[i, 2] == 0 ? false : true;
            lights[i].Setup(); // dla pewności że dobrze wystartuje
        }
    }

    void Update()
    {
        
    }
}
