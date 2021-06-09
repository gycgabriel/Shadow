using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script to control the slime objects
public class AggroSlimeController : MonoBehaviour
{
    public float moveSpeed;                 //The slime's movement speed

    private CircleCollider2D circleCollider;
    private Rigidbody2D myRigidBody;        //The slime's Rigidbody2D component
    public LayerMask blockingLayer;            //Layer on which collision will be checked.

    public bool slimeMoving;               //Whether the slime is moving

    public float timeBetweenMove;           //The amount of time between each movement the slime makes
    public float timeBetweenMoveCounter;   //The time counter for time between movements
    public float timeToMove;                //The amount of time the slime takes to move
    private float timeToMoveCounter;        //The time counter for the slime's movement

    private Vector2 moveDirection;          //The movement direction vector of the slime
    private Vector2[] moveDirections = new Vector2[] 
    {
        new Vector2(0f,1f), new Vector2(1f,0f), new Vector2(0f,-1f), new Vector2(-1f,0f)
    };

    private PlayerController player;
    public Animator anim;

    public GameObject MonsterAlert;
    private bool isAlert;
    public float detectionRange;

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to this object's Rigidbody2D
        myRigidBody = GetComponent<Rigidbody2D>();

        circleCollider = GetComponent<CircleCollider2D>();

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
        //Check if the slime is moving
        //if (!slimeMoving)
        //{
            //If the slime is not moving, decrement the time between move counter
            timeBetweenMoveCounter -= Time.deltaTime;

            //Set the slime's velocity to zero
            //myRigidBody.velocity = Vector2.zero;

            //Check if the slime finished waiting between movements

        if (!isAlert && PlayerInLOS())
        {
            Instantiate(MonsterAlert, transform.position + new Vector3(0f,0.25f,0f), Quaternion.Euler(Vector3.zero));
            isAlert = true;

        }

        if (timeBetweenMoveCounter <= 0f)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= 1f)
            {
                anim.SetFloat("LastMoveX", player.transform.position.x - transform.position.x);
                anim.SetFloat("LastMoveY", player.transform.position.y - transform.position.y);
                anim.SetTrigger("attack");
                //StartCoroutine(Attack());
                timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
            }
            else
            {

                //If the slime is ready to move, set slimeMoving to true
                //slimeMoving = true;

                //Reset the slime's move time counter with some random variation
                //timeToMoveCounter = Random.Range(0.75f, 1.25f) * timeToMove;

                //Randomly select a direction for the slime to move
                moveDirection = moveDirections[Random.Range(0, 4)];

                //transform.position += new Vector3(moveDirection.x, moveDirection.y, 0);

                anim.SetFloat("LastMoveX", moveDirection.x);
                anim.SetFloat("LastMoveY", moveDirection.y);

                RaycastHit2D hit;
                Move((int)moveDirection.x, (int)moveDirection.y, out hit);

                timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;

                //slimeMoving = false;
            }
         }
        //}
    }

    void StopAttack()
    {
        anim.SetTrigger("stopAttack");
        timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * timeBetweenMove;
    }

    bool PlayerInLOS()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= detectionRange &&
            Vector2.Dot(Vector3.Normalize(player.transform.position - transform.position), moveDirection) > 0.7;
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
            StartCoroutine(SmoothMovement(end));

            //Return true to say that Move was successful
            return true;
        }

        //If something was hit, return false, Move was unsuccesful.
        Debug.Log("Slime collided with " + hit.collider.name);
        return false;
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end)
    {
        anim.SetTrigger("move");

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPosition = Vector3.MoveTowards(myRigidBody.position, end, moveSpeed * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            myRigidBody.MovePosition(newPosition);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        anim.SetTrigger("stopMove");
    }

    

}
