using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using MinotaurStates;

public class MinotaurAI : EnemyAI
{
    public int currentPhase;

    public List<string> attackPattern;
    public int attackPatternCounter;

    public List<string> attackPatternPhase1;
    public List<string> attackPatternPhase2;
    public List<string> attackPatternPhase3;

    public float timeBetweenMovePhase2;
    public float timeBetweenMovePhase3;
    public new StateMachine<MinotaurAI> stateMachine { get; set; }

    protected override void Start()
    {
        stateMachine = new StateMachine<MinotaurAI>(this);
        stateMachine.ChangeState(IdleState.Instance);

        // Get a component references
        anim = GetComponentInChildren<Animator>();
        player = PartyController.playerPC;

        // Monster is facing player by default
        /*moveDirection = player.transform.position - transform.position;
        anim.SetFloat("LastMoveX", moveDirection.x);
        anim.SetFloat("LastMoveY", moveDirection.y);*/

        currentPhase = 1;
        attackPattern = attackPatternPhase1;
        attackPatternCounter = 0;
    }

    protected override void Update()
    {
        player = PartyController.activePC;
        stateMachine.Update();
    }

    public override void StopAttack()
    {
        stateMachine.ChangeState(IdleState.Instance);
    }

    public Vector3 GetPosition()
    {
        return transform.position + new Vector3(-0.5f, 0.5f);
    }

    public string GetNextAttackTrigger()
    {
        string nextAttackTrigger = attackPattern[attackPatternCounter++];
        if (attackPatternCounter == attackPattern.Count)
        {
            attackPatternCounter = 0;
        }
        return nextAttackTrigger;
    }

    public void CheckPhase()
    {
        float currentHP = GetComponent<Enemy>().currentHP;
        float maxHP = GetComponent<Enemy>().getStats()["hp"];
        // Debug.Log("Checking Phase: currentHP - " + currentHP + ", maxHP - " + maxHP);
        if (currentPhase == 1 && currentHP < maxHP * (2f/3f))
        {
            EnterPhase2();
            return;
        }
        else if (currentPhase == 2 && currentHP < maxHP * (1f/3f))
        {
            EnterPhase3();
            return;
        }
    }

    public void EnterPhase2()
    {
        Debug.Log("Entering Phase 2");
        currentPhase = 2;
        attackPattern = attackPatternPhase2;
        attackPatternCounter = 0;
        stateMachine.ChangeState(MinotaurStates.AttackState.Instance);
        timeBetweenMove = timeBetweenMovePhase2;
    }
    public void EnterPhase3()
    {
        Debug.Log("Entering Phase 3");
        currentPhase = 3;
        attackPattern = attackPatternPhase3;
        attackPatternCounter = 0;
        stateMachine.ChangeState(MinotaurStates.AttackState.Instance);
        timeBetweenMove = timeBetweenMovePhase3;
    }
}
