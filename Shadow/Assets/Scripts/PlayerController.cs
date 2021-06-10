using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public GameObject cameraController;

    public float moveSpeed;
    public Vector3 position;
    public Transform movePoint;

    public Animator anim;
    public Rigidbody2D myRigidBody;
    public BoxCollider2D boxCollider;
    public LayerMask blockingLayer;            // tilemap layers of non-passable objects

    //private bool pauseMovementInput;
    public bool playerMoving;
    public Vector2 currentMove;
    public Vector2 lastMove;

    // Future: Player Animation.cs?
    private bool playerGoingToAttack;
    private bool playerAttacking;
    public float attackTime;
    //public float attackTimeCounter;

    //public Transform spellFirePoint;          // The point where the fireball will be generated at    
    //public Fireball fireballPrefab;           // The fireball object to be launched

    
    void Start()
    {
        movePoint.parent = null;
        lastMove = new Vector2(0f, -1f);           // player face down 
    }
    void Update()
    {
        if (cameraController == null)
        {
            cameraController = FindObjectOfType<CameraController>().gameObject;
        }


        if (!PauseMenu.gameIsPaused)
        {
            playerMoving = true;

            if (!playerAttacking)
            {
                Move(movePoint.position);

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

                    // Grid-based movement
                    else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                    {
                        UpdateMovePoint(Input.GetAxisRaw("Horizontal"), 0f);
                    }
                    else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                    {
                        UpdateMovePoint(0f, Input.GetAxisRaw("Vertical"));
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

    /** 
     * Checks if possible to move to movePoint.
     */
    private bool CanMove(Vector3 dest)
    {
        Vector2 start = transform.position;
        boxCollider.enabled = false;                                            // linecast doesn't hit this object's own collider
        RaycastHit2D hit = Physics2D.Linecast(start, dest, blockingLayer);      // create linecast from player to intended move point
        boxCollider.enabled = true;
        if (hit.transform != null)
        {
            Debug.Log("Collided with " + hit.collider.name);
        }
        return (hit.transform == null);
    }


    /** 
     * Move returns true if it is able to move and false if not. 
     * Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
     */
    public void Move(Vector3 dest)
    {
        if (CanMove(dest))
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);
        } 
        else
        {
            movePoint.position = transform.position;
        }
    }

    private void UpdateMovePoint(float x, float y)
    {
        movePoint.position += new Vector3(x, y, 0f);
        currentMove = new Vector2(x, y);
        lastMove = currentMove;
    }

    public override void Destroy()
    {
        Destroy(movePoint.gameObject);
        Destroy(gameObject);
    }

    public void SetPosition(Vector3 coords, Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            direction = lastMove;
        }

        this.transform.position = coords;
        this.movePoint.position = coords;
        this.lastMove = direction;
        cameraController.transform.position = new Vector3(transform.position.x, transform.position.y, cameraController.transform.position.z);
        // z-axis no change as camera must maintain a distance away
    }


}
