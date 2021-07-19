using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{

    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;


    void FixedUpdate()
    {
        followTarget = PartyController.gameInstance;

        //Position vector of the target object
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        
        //Linearly interpolate distance to be travelled by the camera according to its speed and translate it
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
