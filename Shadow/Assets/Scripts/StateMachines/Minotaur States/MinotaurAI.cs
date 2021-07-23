using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;
using MinotaurStates;

public class MinotaurAI : EnemyAI
{
    public int numOfPhases;
    [System.NonSerialized]
    public int currentPhase;

    [System.NonSerialized]
    public List<string> attackPattern;
    [System.NonSerialized]
    public int attackPatternCounter;

    public AttackPatternInfo[] attackPatterns;

    public float[] timeBetweenMoveArray;
    public new StateMachine<MinotaurAI> stateMachine { get; set; }

    protected override void Start()
    {
        stateMachine = new StateMachine<MinotaurAI>(this);

        // If final quest is active, wait for dialogue to end before becoming active.
        if (PartyController.quest.title == "Time to dungeon!" && PartyController.quest.isActive)
        {
            stateMachine.ChangeState(UnalertState.Instance);
        }
        else
        {
            stateMachine.ChangeState(IdleState.Instance);
        }

        // Get a component references
        anim = GetComponentInChildren<Animator>();
        player = PartyController.playerPC;

        // Monster is facing player by default
        /*moveDirection = player.transform.position - transform.position;
        anim.SetFloat("LastMoveX", moveDirection.x);
        anim.SetFloat("LastMoveY", moveDirection.y);*/

        currentPhase = 1;
        attackPattern = attackPatterns[0].attackPattern;
        attackPatternCounter = 0;
        timeBetweenMove = timeBetweenMoveArray[0];
    }

    protected override void Update()
    {
        player = PartyController.activePC;
        stateMachine.Update();
    }

    public void StartBossFight()
    {
        stateMachine.ChangeState(IdleState.Instance);
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

    public bool CheckForNextPhase()
    {
        if (currentPhase == numOfPhases)
        {
            return false;
        }

        int currentHP = GetComponent<Enemy>().currentHP;
        int maxHP = GetComponent<Enemy>().getStats()["hp"];

        return currentHP <= maxHP * (numOfPhases - currentPhase) / numOfPhases;
    }
}
