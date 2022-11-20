using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWater : MonoBehaviour
{

    public GameObject water;
    public  Animator an_water;
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 1)
        {
            water.SetActive(false);
            timer = 0f; // ∂® ±2√Î
        }

        
    }

    

    IEnumerator MyMethod()
    {
        
        Debug.Log("Before Waiting 0.5 seconds");
        yield return new WaitForSeconds(1);
        
        Debug.Log("After Waiting 0.5 Seconds");
        
    }
}