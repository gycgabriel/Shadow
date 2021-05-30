using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for Camera Controller
public class CameraController : MonoBehaviour
{
    
    public GameObject followTarget;             //The target object that the camera is following
    private Vector3 targetPos;                  //The position vector of the target object
    public float moveSpeed;                     //The movement speed of the camera

    private static bool cameraExists;           //Whether the camera has been generated

    // Start is called before the first frame update
    void Start()
    {
        //Only one instance of the camera will exist at any given time
        //Check if the camera exists (has been generated already)
        if (!cameraExists)
        {
            //If the camera has not been generated yet, generate the camera
            //Set cameraExists to true so no more cameras will be generated
            cameraExists = true;

            //The camera will persist even when a different scene is loaded
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            //If the camera already exists, do not generate another camera, destroy this one
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Position vector of the target object
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        
        //Linearly interpolate distance to be travelled by the camera according to its speed and translate it
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    public void DestroyCamera()
    {
        cameraExists = false;
        Destroy(gameObject);
    }
}
