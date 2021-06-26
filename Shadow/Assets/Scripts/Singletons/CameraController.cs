using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for Camera Controller
public class CameraController : Singleton<CameraController>
{
    
    public GameObject followTarget;             //The target object that the camera is following
    private Vector3 targetPos;                  //The position vector of the target object
    public float moveSpeed;                     //The movement speed of the camera


    // Start is called before the first frame update
    void Start()
    {
        followTarget = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Position vector of the target object
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        
        //Linearly interpolate distance to be travelled by the camera according to its speed and translate it
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
