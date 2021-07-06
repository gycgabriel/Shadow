using UnityEngine;
using StateStuff;

public class NonAlertIdleState : State<EnemyAI>
{
    private static NonAlertIdleState _instance;

    private NonAlertIdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static NonAlertIdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new NonAlertIdleState();
            }

            return _instance;
        }
    }

    public override void EnterState(EnemyAI _owner)
    {
        // Debug.Log("Entering Non-Alert Idle State");

        _owner.isAlert = false;

        // Set the time counters to their respective times, but with some random variation
        // So not all monsters move at once
        _owner.timeBetweenMoveCounter = Random.Range(0.75f, 1.25f) * _owner.timeBetweenMove;
    }

    public override void ExitState(EnemyAI _owner)
    {
        // Debug.Log("Exiting Non-Alert Idle State");
    }

    public override void UpdateState(EnemyAI _owner)
    {
        // Enter Alert state is player is in sight
        if (PlayerInLOS(_owner))
        {
            _owner.stateMachine.ChangeState(AlertIdleState.Instance);
        }

        // Update Movement Timer
        _owner.timeBetweenMoveCounter -= Time.deltaTime;

        if (_owner.timeBetweenMoveCounter <= 0f)
        {
            _owner.stateMachine.ChangeState(MoveState.Instance);
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
