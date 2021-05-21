using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the start point where the Player spawns at on a new scene
public class PlayerStartPoint : MonoBehaviour
{
    private PlayerController thePlayer;         //The PlayerController of the Player sprite
    private CameraController theCamera;         //The CameraController of the camera

    public Vector2 startDirection;              //The direction the Player faces when spawned

    public string pointName;                    //The name of this start point

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the PlayerController of the Player sprite
        thePlayer = FindObjectOfType<PlayerController>();

        //Check if the Player is spawning at this start point
        if (thePlayer.startPoint == pointName)
        {
            //If the Player is spawning at this start point, set the Player's position to this point
            thePlayer.transform.position = transform.position;

            //Set the Player to face the startDirection
            thePlayer.lastMove = startDirection;

            //Get a reference to the CameraController object in the scene
            theCamera = FindObjectOfType<CameraController>();

            //Set the camera's position in the x,y axis to this point
            //The z-axis is untouched as the camera must maintain a distance away to view the level
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
