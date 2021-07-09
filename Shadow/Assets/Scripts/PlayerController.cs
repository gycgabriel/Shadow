using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject cameraController;

    public float moveSpeed;
    public Transform movePoint;

    public Animator anim;
    public Rigidbody2D myRigidBody;
    public BoxCollider2D boxCollider;
    public LayerMask blockingLayer;            // tilemap layers of non-passable objects
    public LayerMask interactableLayer;         // layer for all dialogue/event triggering interactables
    public SkillSet playerSkillSet;

    public float regenInterval;
    private float regenCounter;

    // Anim variables
    public bool playerMoving;
    public Vector2 currentMove;
    public Vector2 lastMove;
    public bool playerGoingToAttack;
    public bool playerGoingToUltimate;
    public bool playerAttacking;

    void Start()
    {
        movePoint.parent = null;
        if (lastMove == Vector2.zero)
        {
            lastMove = new Vector2(0f, -1f);           // player face down 
        }
    }

    private void Update()
    {
        regenCounter -= Time.deltaTime;
        if (regenCounter <= 0)
        {
            PassiveRegenOverTime();
            regenCounter = regenInterval;
        }
    }

    private void FixedUpdate()
    {
        Move(movePoint.position);
    }

    public void Dash(bool dashInput)
    {
        if (dashInput)
        {
            moveSpeed = 18f;
        }
        else
        {
            moveSpeed = 10f;
        }
    }

    public void HandleInput(Vector2 movement, bool attackInput, bool ultimateInput, bool switchToShadowInput)
    {
        if (cameraController == null)
        {
            cameraController = FindObjectOfType<CameraController>().gameObject;
        }

        if (PauseMenu.gameIsPaused)
        {
            // If game is paused, freeze the player
            return;
        }

        if (playerAttacking)
        {
            // When player is attacking, player can continue to attack but cannot move or change to shadow
            HandleSkillsInput(attackInput, ultimateInput);
            return;
        }

        // Only handle other inputs when player is at target position
        if (Vector3.Distance(transform.position, movePoint.position) <= float.Epsilon)
        {

            if (attackInput)
            {
                GameObject interactObject = GetInteractable();

                if (interactObject != null && !Singleton<DialogueManager>.scriptInstance.dialogueBox.activeSelf)
                {
                    interactObject.GetComponent<Interactable>().Interact();
                    return;
                }
            }

            HandleSkillsInput(attackInput, ultimateInput);

            if (!playerAttacking)
            {
                // Grid-based movement
                HandleMovementInput(movement);
                HandleChangeShadowInput(switchToShadowInput);
            }
        }

        // Update anim vars
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

    /**
     * To be called by SpriteRenderer Component when attack animation ends
     */
    public void StopAttack()
    {
        playerAttacking = false;
    }

    private void HandleSkillsInput(bool attackInput, bool ultimateInput)
    {
        // Player can use ultimate skill only when player is not in the middle of attacking,
        // and ultimate skill is not on cooldown.
        if (!playerAttacking && ultimateInput && GetComponent<Player>().currentMP >= playerSkillSet.ultimateSkill.skillMPCost
            && !PartyController.skillsUIManager.IsUltimateSkillOnCooldown(PartyController.shadowActive))
        {
            playerMoving = false;
            playerAttacking = true;
            playerSkillSet.UltimateSkill();
            PartyController.skillsUIManager.UseUltimateSkill(PartyController.shadowActive);
        }
        else if (attackInput)
        {
            playerMoving = false;
            playerAttacking = true;
            playerSkillSet.NormalAttack();
        }
    }

    private void HandleMovementInput(Vector2 movement)
    {
        // Grid-based Movement
        if (Mathf.Abs(movement.x) == 1f)
        {
            playerMoving = true;
            UpdateMovePoint(movement.x, 0f);
        }
        else if (Mathf.Abs(movement.y) == 1f)
        {
            playerMoving = true;
            UpdateMovePoint(0f, movement.y);
        }
        else
        {
            playerMoving = false;
            currentMove = Vector2.zero;
        }
    }
    private void HandleChangeShadowInput(bool switchToShadowInput)
    {
        if (switchToShadowInput)
        {
            playerMoving = false;
            PartyController.SwitchShadow();
        }
    }

    /** 
     * Checks if possible to move to movePoint.
     */
    private bool CanMove(Vector3 dest)
    {
        Vector2 start = transform.position;
        boxCollider.enabled = false;                                            // linecast doesn't hit this object's own collider

        // Create linecast from player to the middle and the four edges of the intended move point
        RaycastHit2D[] hit = new RaycastHit2D[5];
        hit[0] = Physics2D.Linecast(start, dest, blockingLayer);                            // Center
        hit[1] = Physics2D.Linecast(start, dest + new Vector3(0f, 0.495f), blockingLayer);    // Top edge
        hit[2] = Physics2D.Linecast(start, dest + new Vector3(0f, -0.495f), blockingLayer);   // Bottom edge
        hit[3] = Physics2D.Linecast(start, dest + new Vector3(0.495f, 0f), blockingLayer);    // Right edge
        hit[4] = Physics2D.Linecast(start, dest + new Vector3(-0.495f, 0f), blockingLayer);   // Left edge

        boxCollider.enabled = true;

        bool canMove = true;
        for (int i = 0; i < 5; i++)
        {
            if (hit[i].transform != null)
            {
                // Obstacle detected, unable to move to destination
                //Debug.Log("Player collided with " + hit[i].collider.name);
                canMove = false;
                break;
            }
        }

        return canMove;
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
            float newX = RoundToNearestGrid(transform.position.x);
            float newY = RoundToNearestGrid(transform.position.y);
            transform.position = new Vector3(newX, newY);
            movePoint.position = transform.position;
        }
    }

    private void UpdateMovePoint(float x, float y)
    {
        float newX = RoundToNearestGrid(movePoint.position.x + x);
        float newY = RoundToNearestGrid(movePoint.position.y + y);
        movePoint.position = new Vector3(newX, newY);
        currentMove = new Vector2(x, y);
        lastMove = currentMove;
    }

    public void Destroy()
    {
        Destroy(movePoint.gameObject);
        Destroy(gameObject);
    }

    public void SetPosition(Vector3 coords, Vector2 direction)
    {
        if (cameraController == null)
        {
            cameraController = CameraController.gameInstance;
        }

        if (direction == Vector2.zero)
        {
            direction = lastMove;
        }

        this.transform.position = coords;
        this.movePoint.position = coords;
        this.lastMove = direction;

        if (cameraController != null)
        {
            cameraController.transform.position = new Vector3(transform.position.x, transform.position.y, cameraController.transform.position.z);
        }
        // z-axis no change as camera must maintain a distance away
    }

    public GameObject GetInteractable()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, lastMove, 1.5f, interactableLayer);
        return (ray.collider == null) ? null : ray.collider.gameObject;
    }

    void PassiveRegenOverTime()
    {
        Player player = GetComponent <Player>();
        if (player.currentHP < player.getStats()["hp"])
        {
            player.currentHP += Mathf.FloorToInt(0.2f * player.getStats()["hp"]);
            player.currentHP = Mathf.Min(player.currentHP, player.getStats()["hp"]);
        }
        if (player.currentMP < player.getStats()["mp"])
        {
            player.currentMP += Mathf.FloorToInt(0.2f * player.getStats()["mp"]);
            player.currentMP = Mathf.Min(player.currentMP, player.getStats()["mp"]);
        }
    }

    /**
     * Rounds the coordinate to nearest 0.5, to be on grid.
     */
    float RoundToNearestGrid(float x)
    {
        return (float)Mathf.Round(x - 0.5f) + 0.5f;
    }
}
