using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple script for setting a timer before being destroying the object
public class DestroyOverTime : MonoBehaviour
{
    public float timeToDestroy;     //Amount of time before being destroyed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Decrement time counter
        timeToDestroy -= Time.deltaTime;
        
        //Once time is up, destroy the object
        if (timeToDestroy < 0)
        {
            Destroy(gameObject);
        }
    }
}
