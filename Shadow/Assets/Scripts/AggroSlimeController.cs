using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script to control the slime objects
public class AggroSlimeController : MonoBehaviour
{
    public float moveSpeed;                 // The slime's movement speed

    private CircleCollider2D circleCollider;
    private Rigidbody2D myRigidBody;        // The slime's Rigidbody2D component
    public LayerMask blockingLayer;         // Layer on which collision will be checked.

    public bool slimeMoving;                // Whether the slime is moving or attacking

    public float timeBetweenMove;           // The amount of time between each movement the slime makes
    public float timeBetweenMoveCounter;    // The time counter for time between movements
    public float timeToMove;                // The amount of time the slime takes to move
    private float timeToMoveCounter;        // The time counter for the slime's movement

    private Vector2 moveDirection;          // The movement direction vector of the slime
    private Vector2[] moveDirections = new Vector2[] 
    {
        new Vector2(0f,1f), new Vector2(1f,0f), new Vector2(0f,-1f), new Vector2(-1f,0f)
    };

    private PlayerController player;
    private Animator anim;

    public GameObject MonsterAlertOn;
    public GameObject MonsterAlertOff;
    private bool isAlert;
    public float alertTime;
    private float alertTimeCounter;
    public float detectionRange;

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to this object's Rigidbody2D
        myRigidBody = GetComponentInChildren<Rigidbody2D>();

        circleCollider = GetComponentInChildren<CircleCollider2D>();

        anim = GetComponentInChildren<Animator>();

        //Set the time counters to their respective times, but with some random variation
        //So not all slimes move at once and take the same amount of time to move
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
        //timeToMoveCounter = Random.Range(0.75f, 1.25f) * timeToMove;

        player = FindObjectOfType<PlayerController>();

        anim.SetFloat("LastMoveX", 0f);
        anim.SetFloat("LastMoveY", -1f);

        isAlert = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlert && PlayerInLOS())
        {
            // Monster is alerted to the player
            Instantiate(MonsterAlertOn, transform.position + new Vector3(0f,0.25f,0f), Quaternion.Euler(Vector3.zero));
            isAlert = true;
            alertTimeCounter = alertTime;
        }
        else if (isAlert)
        {
            if (PlayerInLOS())
            {
                // The monster's alertness is refreshed upon seeing the player
                alertTimeCounter = alertTime;
            }
            else
            {
                // The monster's alertness is reduced when not seeing the player
                alertTimeCounter -= Time.deltaTime;

                if (alertTimeCounter <= 0)
                {
                    // The monster loses track of the player and is no longer on alert
                    Instantiate(MonsterAlertOff, transform.position + new Vector3(0f, 0.25f, 0f), Quaternion.Euler(Vector3.zero));
                    isAlert = false;
                }
            }
        }

        if (!slimeMoving)
        {
            // Decrement the time between move counter only if the slime is not moving/attacking
            timeBetweenMoveCounter -= Time.deltaTime;

            if (timeBetweenMoveCounter <= 0f)
            {
                slimeMoving = true;

                if (isAlert)
                {
                    // If monster is alert for the player,

                    moveDirection = player.transform.position - transform.position;
                    if (moveDirection.sqrMagnitude <= 1f)
                    {
                        // Monster will attack the player if in attack range
                        anim.SetFloat("LastMoveX", moveDirection.x);
                        anim.SetFloat("LastMoveY", moveDirection.y);
                        anim.SetTrigger("attack");
                    }
                    else
                    {
                        // Monster will move towards the player if not in attack range
                        // Attempt to move horizontally towards the player, if unable to move then move vertically
                        RaycastHit2D hit;
                        bool moveSuccessful = false;
                        Vector2 movedDirection = Vector2.zero;

                        if (moveDirection.x != 0)
                        {
                            if (moveDirection.x > 0)
                                movedDirection = new Vector2(1, 0);
                            else
                                movedDirection = new Vector2(-1, 0);
                            moveSuccessful = Move((int)movedDirection.x, 0, out hit);
                        }

                        if (moveDirection.y != 0 && !moveSuccessful)
                        {
                            if (moveDirection.y > 0)
                                movedDirection = new Vector2(0, 1);
                            else
                                movedDirection = new Vector2(0, -1);
                            Move(0, (int)movedDirection.y, out hit);
                        }

                        anim.SetFloat("LastMoveX", movedDirection.x);
                        anim.SetFloat("LastMoveY", movedDirection.y);
                    }
                }
                else
                {
                    //Randomly select a direction for the slime to move
                    moveDirection = moveDirections[Random.Range(0, 4)];

                    anim.SetFloat("LastMoveX", moveDirection.x);
                    anim.SetFloat("LastMoveY", moveDirection.y);

                    RaycastHit2D hit;
                    Move((int)moveDirection.x, (int)moveDirection.y, out hit);
                }
             }
        }
    }

    public void StopAttack()
    {
        anim.SetTrigger("stopAttack");
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
        slimeMoving = false;
    }

    // Check whether the player is in the monster's Line Of Sight (LOS)
    bool PlayerInLOS()
    {
        Vector2 LOSDirection = new Vector2(anim.GetFloat("LastMoveX"), anim.GetFloat("LastMoveY"));
        Vector2 playerDirection = player.transform.position - transform.position;

        return playerDirection.magnitude <= detectionRange &&
            Vector2.Dot(playerDirection.normalized, moveDirection) > 0.7;
    }


    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = start + new Vector2(xDir, yDir);

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        circleCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        hit = Physics2D.Linecast(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        circleCollider.enabled = true;

        //Check if anything was hit
        if (hit.transform == null)
        {
            //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
            StartCoroutine(SmoothMovement(start, end));

            //Return true to say that Move was successful
            return true;
        }

        //If something was hit, return false, Move was unsuccesful.
        //Debug.Log("Slime collided with " + hit.collider.name);

        slimeMoving = false;
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
        return false;
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 start, Vector3 end)
    {
        anim.SetTrigger("move");

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (start - end).sqrMagnitude;
        Vector3 newPosition = start;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            newPosition = Vector3.MoveTowards(newPosition, end, moveSpeed * Time.deltaTime);

            //Move the transform to the calculated position.
            transform.position = newPosition;

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (newPosition - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        slimeMoving = false;
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
        anim.SetTrigger("stopMove");
    }

    

}
