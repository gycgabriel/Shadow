using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D monsterRigidBody;
    public Collider2D monsterGridCollider;
    public LayerMask blockingLayer;         // Layer on which collision will be checked.

    public float moveSpeed;                 // The monster's movement speed
    public float timeBetweenMove;           // The amount of time between each movement the monster makes
    
    [System.NonSerialized]
    public Vector2 moveDirection;          // The movement direction vector of the slime
    [System.NonSerialized]
    public Vector3 dest;

    [System.NonSerialized]
    public PlayerController player;
    [System.NonSerialized]
    public Animator anim;

    [System.NonSerialized]
    public bool isAlert;
    public GameObject MonsterAlertOn;
    public GameObject MonsterAlertOff;
    public Vector3 alertOffset;
    public float maxAlertLevel;
    public float detectionRange;
    public float attackRange;

    [System.NonSerialized]
    public float timeBetweenMoveCounter;   // The time counter for time between movements
    [System.NonSerialized]
    public float alertLevel;

    public EnemyUIManager enemyUIManager;
    public Canvas enemyStatusCanvas;

    [System.NonSerialized]
    public TargetEnemyUIManager targetEnemyUIManager;

    public StateMachine<EnemyAI> stateMachine { get; set; }

    protected virtual void Start()
    {
        stateMachine = new StateMachine<EnemyAI>(this);
        stateMachine.ChangeState(NonAlertIdleState.Instance);

        // Get a component references
        anim = GetComponentInChildren<Animator>();
        player = PartyController.playerPC;
        targetEnemyUIManager = Singleton<TargetEnemyUIManager>.scriptInstance;

        // Monster is facing down by default
        moveDirection = new Vector2(0f, -1f);
        anim.SetFloat("LastMoveX", moveDirection.x);
        anim.SetFloat("LastMoveY", moveDirection.y);

        enemyUIManager.enabled = false;
        enemyStatusCanvas.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        // Ensure rigidbody doesnt get desynced
        Transform rbTransform = monsterRigidBody.transform;
        Transform gcTransform = monsterGridCollider.transform;
        if (rbTransform.position != gcTransform.position)
            rbTransform.position = gcTransform.position;

        player = PartyController.activePC;
        targetEnemyUIManager = Singleton<TargetEnemyUIManager>.scriptInstance;
        stateMachine.Update();
    }

    public virtual void StopAttack()
    {
        stateMachine.ChangeState(AlertIdleState.Instance);
    }

    /**
     * Cleanup after the enemy is dead
     */
    void OnDisable()
    {
        // Stop EnemyUIManager from tracking its status anymore
        if (targetEnemyUIManager != null)
        {
            targetEnemyUIManager.removeAlertedEnemy(GetComponent<Enemy>());
        }
    }

    public void AlertOn()
    {
        if (!isAlert)
        {
            isAlert = true;
            alertLevel = maxAlertLevel;

            Instantiate(MonsterAlertOn, this.transform.position + alertOffset, Quaternion.Euler(Vector3.zero));
            if (targetEnemyUIManager != null)
            {
                targetEnemyUIManager.addAlertedEnemy(GetComponent<Enemy>());
            }

            // Display mini HP bar above the enemy
            enemyUIManager.enabled = true;
            enemyStatusCanvas.gameObject.SetActive(true);
            
            // The monster faces the direction of the player upon being alerted
            moveDirection = player.transform.position - this.transform.position;
            anim.SetFloat("LastMoveX", moveDirection.x);
            anim.SetFloat("LastMoveY", moveDirection.y);
        }
    }

    public void AlertOff()
    {
        Instantiate(MonsterAlertOff, this.transform.position + alertOffset, Quaternion.Euler(Vector3.zero)); ;
        if (targetEnemyUIManager != null)
        {
            targetEnemyUIManager.removeAlertedEnemy(GetComponent<Enemy>());
        }

        // Disable mini HP bar above the enemy
        enemyUIManager.enabled = false;
        enemyStatusCanvas.gameObject.SetActive(false);
    }
}
