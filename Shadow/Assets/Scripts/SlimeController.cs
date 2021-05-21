using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script to control the slime objects
public class SlimeController : MonoBehaviour
{
    public float moveSpeed;                 //The slime's movement speed

    private Rigidbody2D myRigidBody;        //The slime's Rigidbody2D component

    private bool slimeMoving;               //Whether the slime is moving

    public float timeBetweenMove;           //The amount of time between each movement the slime makes
    private float timeBetweenMoveCounter;   //The time counter for time between movements
    public float timeToMove;                //The amount of time the slime takes to move
    private float timeToMoveCounter;        //The time counter for the slime's movement

    private Vector2 moveDirection;          //The movement direction vector of the slime

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to this object's Rigidbody2D
        myRigidBody = GetComponent<Rigidbody2D>();

        //Set the time counters to their respective times, but with some random variation
        //So not all slimes move at once and take the same amount of time to move
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
        timeToMoveCounter = Random.Range(0.75f, 1.25f) * timeToMove;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the slime is moving
        if (slimeMoving)
        {
            //If the slime is moving, decrement the move time counter
            timeToMoveCounter -= Time.deltaTime;

            //Set the slime's velocity to direction * speed
            myRigidBody.velocity = moveDirection * moveSpeed;

            //Check if the slime still has time for moving
            if (timeToMoveCounter < 0)
            {
                //If the slime ran out of time to move, set slimeMoving to false
                slimeMoving = false;

                //Reset the counter for time between move with some random variation
                timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
            }
        } 
        else
        {
            //If the slime is not moving, decrement the time between move counter
            timeBetweenMoveCounter -= Time.deltaTime;

            //Set the slime's velocity to zero
            myRigidBody.velocity = Vector2.zero;

            //Check if the slime finished waiting between movements
            if (timeBetweenMoveCounter < 0f)
            {
                //If the slime is ready to move, set slimeMoving to true
                slimeMoving = true;

                //Reset the slime's move time counter with some random variation
                timeToMoveCounter = Random.Range(0.75f, 1.25f) * timeToMove;

                //Randomly select a direction for the slime to move
                moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
        }
    }

}
