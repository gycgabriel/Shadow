using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public Transform movePoint;

    public Animator anim;
    public Rigidbody2D myRigidBody;
    public BoxCollider2D boxCollider;         //The BoxCollider2D component attached to this object.
    public LayerMask blockingLayer;            //Layer on which collision will be checked.

    //private bool pauseMovementInput;
    public bool playerMoving;
    public Vector2 currentMove;
    public Vector2 lastMove;

    private static bool playerExists;

    private bool playerGoingToAttack;
    private bool playerAttacking;
    public float attackTime;
    //public float attackTimeCounter;

    //public Transform spellFirePoint;          //The point where the fireball will be generated at    
    //public Fireball fireballPrefab;           //The fireball object to be launched

    public string startPoint;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;

        //anim = GetComponent<Animator>();
        //myRigidBody = GetComponent<Rigidbody2D>();
        //boxCollider = GetComponent<BoxCollider2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(movePoint.gameObject);
        }
        else
        {
            Destroy(movePoint.gameObject);
            Destroy(gameObject);  
        }

        lastMove = new Vector2(0f, -1f);           //spawn the player initially facing down 
    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseMenu.gameIsPaused)
        {
            playerMoving = true;

            if (!playerAttacking)
            {
                //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
                if (!Move(movePoint.position, out RaycastHit2D hit))
                {
                    movePoint.position = transform.position;
                }

                if (Vector3.Distance(transform.position, movePoint.position) <= float.Epsilon)
                {
                    if (playerGoingToAttack)
                    {
                        //attackTimeCounter = attackTime;
                        //playerAttacking = true;
                        StartCoroutine(StartAttackTimer());

                        //StartCoroutine(CastFireball());

                        playerGoingToAttack = false;
                    }
                    else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                    {
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        currentMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
                        lastMove = currentMove;
                    }
                    else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                    {
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        currentMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
                        lastMove = currentMove;
                    }
                    else
                    {
                        currentMove = Vector2.zero;
                        playerMoving = false;
                    }

                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        playerGoingToAttack = true;
                    }
                }



            }
            /*
            if ()
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
            */

            if (!currentMove.Equals(Vector2.zero))
            {
                anim.SetFloat("MoveX", currentMove.x);
                anim.SetFloat("MoveY", currentMove.y);
            }
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
            anim.SetBool("PlayerMoving", playerMoving);
            anim.SetBool("PlayerAttacking", playerAttacking);
        }


    }

    IEnumerator StartAttackTimer()
    {
        playerAttacking = true;
        yield return new WaitForSeconds(attackTime);
        playerAttacking = false;
    }

    
    //Coroutine to summon fireball, Sorcerer's normal attack
    /*
    IEnumerator CastFireball()
    {
        yield return new WaitForSeconds(0.3f);
        Instantiate(fireballPrefab, spellFirePoint.position, spellFirePoint.rotation);
    }
    */

    /*IEnumerator MovePlayer()
    {

    }*/

    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
    protected bool Move(Vector3 end, out RaycastHit2D hit)
    {
        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        //Vector2 end = start + new Vector2(xDir, yDir);

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        boxCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        hit = Physics2D.Linecast(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        boxCollider.enabled = true;

        //Check if anything was hit
        if (hit.transform == null)
        {
            //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            //Return true to say that Move was successful
            return true;
        }

        //If something was hit, return false, Move was unsuccesful.
        Debug.Log("Collided with " + hit.collider.name);
        return false;
    }


    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end)
    {
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
    }

    public void DestroyPlayer()
    {
        playerExists = false;
        Destroy(movePoint.gameObject);
        Destroy(gameObject);
    }
}
