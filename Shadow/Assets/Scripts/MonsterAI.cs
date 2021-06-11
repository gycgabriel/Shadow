using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to control the moster objects
public class MonsterAI : MonoBehaviour
{
    public float moveSpeed;                 // The monster's movement speed

    private Collider2D monsterCollider;
    public LayerMask blockingLayer;         // Layer on which collision will be checked.

    private bool monsterMoving;                // Whether the monster is moving or attacking
    public float timeBetweenMove;           // The amount of time between each movement the monster makes
    private float timeBetweenMoveCounter;    // The time counter for time between movements

    private Vector2 moveDirection;          // The movement direction vector of the slime
    private Vector2[] moveDirections = new Vector2[] //The four possible move directions
    {
        new Vector2(0f,1f), new Vector2(1f,0f), new Vector2(0f,-1f), new Vector2(-1f,0f)
    };

    private PlayerController player;
    private Animator anim;

    public GameObject MonsterAlertOn;
    public GameObject MonsterAlertOff;
    public Vector3 alertOffset;
    private bool isAlert;
    public float maxAlertLevel;
    private float alertLevel;
    public float detectionRange;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        // Get a component references
        monsterCollider = GetComponentInChildren<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>();

        // Set the time counters to their respective times, but with some random variation
        // So not all monsters move at once
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;

        // Monster is facing down by default
        anim.SetFloat("LastMoveX", 0f);
        anim.SetFloat("LastMoveY", -1f);

        isAlert = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterMoving)
        {
            // If the monster is in the middle of movement, do nothing
            return;
        }

        if (PlayerInLOS())
        {
            // Monster can see and is alerted to the player
            AlertOn();
        }
        else if (isAlert)
        {
            // The monster's alertness is reduced when not seeing the player
            alertLevel -= Time.deltaTime;

            if (alertLevel <= 0)
            {
                AlertOff();
                    
            }
        }


        // Decrement the time between move counter only if the monster is not moving/attacking
        timeBetweenMoveCounter -= Time.deltaTime;

        if (timeBetweenMoveCounter <= 0f)
        {
            if (isAlert)
            {
                // If monster is alert for the player,

                if (Vector2.Distance(player.transform.position, this.transform.position) <= attackRange)
                {
                    // Monster will attack the player if in attack range
                    AttackPlayer();
                }
                else
                {
                    // Monster will move towards the player if not in attack range.
                    MoveTowardsPlayer();
                }
            }
            else
            {
                //Randomly select a direction for the slime to move
                moveDirection = moveDirections[Random.Range(0, 4)];
                Move((int)moveDirection.x, (int)moveDirection.y);
            }

            anim.SetFloat("LastMoveX", moveDirection.x);
            anim.SetFloat("LastMoveY", moveDirection.y);
        }
        
    }

    public void StopAttack()
    {
        anim.SetTrigger("stopAttack");
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
        monsterMoving = false;
    }

    /**
     * The monster is alerted of the player.
     */
    public void AlertOn()
    {
        if (!isAlert)
        {
            // If the monster is not already on alert, show alert signal
            Instantiate(MonsterAlertOn, this.transform.position + alertOffset, Quaternion.Euler(Vector3.zero));
            isAlert = true;
        }
        
        alertLevel = maxAlertLevel;

        // The monster faces the direction of the player upon being alerted
        moveDirection = player.transform.position - this.transform.position;
        anim.SetFloat("LastMoveX", moveDirection.x);
        anim.SetFloat("LastMoveY", moveDirection.y);
    }

    /**
     * The monster is no longer alert for the player.
     */
    public void AlertOff()
    {
        // The monster loses track of the player and is no longer on alert
        Instantiate(MonsterAlertOff, this.transform.position + alertOffset, Quaternion.Euler(Vector3.zero));
        isAlert = false;
    }

    /**
     * Check whether the player is in the monster's Line Of Sight (LOS) by using 
     * dot product of player's direction from the monster and the direction the monster is facing
     */
    bool PlayerInLOS()
    {
        Vector2 playerDirection = player.transform.position - this.transform.position;

        return playerDirection.magnitude <= detectionRange &&
            Vector2.Dot(playerDirection.normalized, moveDirection) > 0.7;
    }

    void AttackPlayer()
    {
        monsterMoving = true;
        moveDirection = player.transform.position - this.transform.position;
        anim.SetTrigger("attack");
    }

    /**
     * Attempt to move horizontally towards the player first, if unable to move then move vertically.
     * return whether move was successful.
     */
    bool MoveTowardsPlayer()
    {
        monsterMoving = true;
        moveDirection = player.transform.position - this.transform.position;

        bool moveSuccessful = false;

        if (moveDirection.x > 0f)
        {
            moveSuccessful = Move(1, 0);
        }
        else if (moveDirection.x < 0f)
        {
            moveSuccessful = Move(-1, 0);
        }

        if (!moveSuccessful)
        {
            if (moveDirection.y > 0f)
            {
                moveSuccessful = Move(0, 1);
            }
            else if (moveDirection.y < 0f)
            {
                moveSuccessful = Move(0, -1);
            }
        }

        return moveSuccessful;
    }

    /** 
     * Checks if possible to move to movePoint.
     */
    private bool CanMove(Vector3 dest)
    {
        Vector2 start = this.transform.position;
        monsterCollider.enabled = false;                                         // linecast doesn't hit this object's own collider
        RaycastHit2D hit = Physics2D.Linecast(start, dest, blockingLayer);      // create linecast from object to intended move point
        monsterCollider.enabled = true;
        if (hit.transform != null)
        {
            //Debug.Log("Collided with " + hit.collider.name);
        }
        return (hit.transform == null);
    }


    /** 
     * Move returns true if it is able to move and false if not. 
     * Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
     */
    public bool Move(int xDir, int yDir)
    {
        Vector3 dest = this.transform.position + new Vector3(xDir, yDir, 0);

        if (CanMove(dest))
        {
            StartCoroutine(SmoothMovement(this.transform.position, dest));
            return true;
        }
        else
        {
            timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
            return false;
        }
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 start, Vector3 end)
    {
        monsterMoving = true;
        anim.SetTrigger("move");

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        float sqrRemainingDistance = (start - end).sqrMagnitude;
        Vector3 newPosition = start;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            newPosition = Vector3.MoveTowards(newPosition, end, moveSpeed * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (newPosition - end).sqrMagnitude;
            yield return null;
        }

        monsterMoving = false;
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
        anim.SetTrigger("stopMove");
    }

    

}
