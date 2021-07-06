using UnityEngine;
using StateStuff;

public class AlertIdleState : State<EnemyAI>
{


    private static AlertIdleState _instance;

    private AlertIdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AlertIdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AlertIdleState();
            }

            return _instance;
        }
    }

    public override void EnterState(EnemyAI _owner)
    {
        // Debug.Log("Entering Alert Idle State");

        _owner.AlertOn();

        // Set the time counters to their respective times, but with some random variation
        // So not all monsters move at once
        _owner.timeBetweenMoveCounter = Random.Range(0.75f, 1f) * _owner.timeBetweenMove;
    }

    public override void ExitState(EnemyAI _owner)
    {
        // Debug.Log("Exiting Alert Idle State");
    }

    public override void UpdateState(EnemyAI _owner)
    {
        // The monster faces the direction of the player upon being alerted
        Vector2 moveDirection = _owner.player.transform.position - _owner.transform.position;
        _owner.moveDirection = moveDirection;
        _owner.anim.SetFloat("LastMoveX", moveDirection.x);
        _owner.anim.SetFloat("LastMoveY", moveDirection.y);

        // Update Alert Level
        if (PlayerInLOS(_owner))
        {
            _owner.alertLevel = _owner.maxAlertLevel;
        }
        else
        {
            _owner.alertLevel -= Time.deltaTime;

            if (_owner.alertLevel <= 0)
            {
                _owner.AlertOff();
                _owner.stateMachine.ChangeState(NonAlertIdleState.Instance);
            }
        }

        // Update Movement Timer
        _owner.timeBetweenMoveCounter -= Time.deltaTime;

        if (_owner.timeBetweenMoveCounter <= 0f)
        {
            if (Vector2.Distance(_owner.player.transform.position, _owner.transform.position) <= _owner.attackRange)
            {
                // Monster will attack the player if in attack range
                _owner.stateMachine.ChangeState(AttackState.Instance); ;
            }
            else
            {
                // Monster will move towards the player if not in attack range.
                _owner.stateMachine.ChangeState(MoveState.Instance);
            }
            
        }
    }

    bool PlayerInLOS(EnemyAI _owner)
    {
        if (_owner.player == null)
        {
            return false;
        }

        Vector2 playerDirection = _owner.player.transform.position - _owner.transform.position;

        return playerDirection.magnitude <= _owner.detectionRange &&
            Vector2.Dot(playerDirection.normalized, _owner.moveDirection) > 0.7;
    }
}
