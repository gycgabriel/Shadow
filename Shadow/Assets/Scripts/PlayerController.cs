using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script controlling the Player's sprite
public class PlayerController : MonoBehaviour
{

    public float moveSpeed;                 //The Player's movement speed

    private Animator anim;                  //The Player's Animator object
    private Rigidbody2D myRigidBody;        //The Player's RigidBody2D object

    private static bool playerExists;       //Whether the Player has been generated

    private bool playerMoving;              //Whether the Player is currently moving
    public Vector2 lastMove;                //The direction vector of the Player's last movement

    private bool playerAttacking;           //Whether the Player is currently attacking
    public float attackTime;                //The time the Player takes to attack
    private float attackTimeCounter;        //The time counter for how long the Player has been attacking 

    public string startPoint;               //The name of the start point that the Player will spawn at when loading the scene

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to this object's Animator
        anim = GetComponent<Animator>();

        //Get a component reference to this object's RigidBody2D
        myRigidBody = GetComponent<Rigidbody2D>();

        //Only one instance of the Player will exist at any given time
        //Check if the Player exists (has been generated already)
        if (!playerExists)
        {
            //If the Player has not been generated yet, generate the Player
            //Set playerExists to true so no more Player objects will be generated
            playerExists = true;

            //The Player will persist even when a different scene is loaded
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            //If the Player already exists, do not generate another Player, destroy this one
            Destroy(gameObject);  
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Set the Player to be not moving by default
        playerMoving = false;

        //Check if the Player is attacking
        if (!playerAttacking)
        {
            //If the Player is not attacking, the Player can receive inputs

            //Check for horizontal input
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
            {
                //If there is horizontal input, set the Player's horizontal velocity to input * movement speed
                myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidBody.velocity.y);

                //Set playerMoving to be true to activate the Player's moving animation
                playerMoving = true;

                //Set lastMove to the direction of the horizontal input to set direction the Player faces once movement stops
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            } 
            else
            {
                //if there is no horizontal input, set the Player's horizontal velocity to zero
                myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
            }

            //Check for vertical input
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                //If there is vertical input, set the Player's vertical velocity to input * movement speed
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);

                //Set playerMoving to be true to activate the Player's moving animation
                playerMoving = true;

                //Set lastMove to the direction of the vertical input to set direction the Player faces once movement stops
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            } 
            else
            {
                //if there is no vertical input, set the Player's vertical velocity to zero
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
            }

            //Check if the Player is moving diagonally
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                //If the Player is moving diagonally, normalize the direction vector so the speed is not boosted
                Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                moveDirection.Normalize();
                moveDirection *= moveSpeed;
                myRigidBody.velocity = moveDirection;
            }

            //Check for attack input
            if (Input.GetKeyDown(KeyCode.J))
            {
                //If there is attack input, set playerAttacking to be true to activate attack animation
                playerAttacking = true;

                //Reset the attack time counter
                attackTimeCounter = attackTime;

                //The player should not be moving while attacking so set the Player's velocity to zero
                myRigidBody.velocity = Vector2.zero;
            }
        }
        else
        {
            //Else if the Player is attacking, decrement the attack time counter
            attackTimeCounter -= Time.deltaTime;

            //Check if the counter time is up
            if (attackTimeCounter <= 0)
            {
                //If the attack time is up, set playerAttacking to false to stop the attack animation
                playerAttacking = false;
            }
        }

        //Setting of the Player's Animator's variables

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal")); //Current horizontal input
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));   //Current vertical input
        anim.SetFloat("LastMoveX", lastMove.x);                 //Previous horizontal input
        anim.SetFloat("LastMoveY", lastMove.y);                 //Previous vertical input
        anim.SetBool("PlayerMoving", playerMoving);             //Whether the Player is moving
        anim.SetBool("PlayerAttacking", playerAttacking);       //Whether the Player is attacking
    }
}
