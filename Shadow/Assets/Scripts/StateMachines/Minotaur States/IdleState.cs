using UnityEngine;
using StateStuff;

namespace MinotaurStates
{
    public class IdleState : State<MinotaurAI>
    {
        private static IdleState _instance;

        private IdleState()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static IdleState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new IdleState();
                }

                return _instance;
            }
        }

        public override void EnterState(MinotaurAI _owner)
        {
            // Debug.Log("Entering Minotaur Idle State");

            // Set the time counters to their respective times, but with some random variation
            // So not all monsters move at once
            _owner.timeBetweenMoveCounter = Random.Range(0.75f, 1f) * _owner.timeBetweenMove;
        }

        public override void ExitState(MinotaurAI _owner)
        {
            // Debug.Log("Exiting Minotaur Idle State");
        }

        public override void UpdateState(MinotaurAI _owner)
        {
            // The monster faces the direction of the player upon being alerted
            Vector2 moveDirection = _owner.player.transform.position - _owner.transform.position;
            _owner.moveDirection = moveDirection;
            _owner.anim.SetFloat("LastMoveX", moveDirection.x);
            _owner.anim.SetFloat("LastMoveY", moveDirection.y);

            if (_owner.CheckForNextPhase())
            {
                _owner.stateMachine.ChangeState(EnterNextPhaseState.Instance);
            }

            // Update Movement Timer
            _owner.timeBetweenMoveCounter -= Time.deltaTime;

            if (_owner.timeBetweenMoveCounter <= 0f)
            {
                if (Vector2.Distance(_owner.player.transform.position, _owner.GetPosition()) <= _owner.attackRange)
                {
                    // Monster will attack the player if in attack range
                    _owner.stateMachine.ChangeState(AttackState.Instance);
                }
                else
                {
                    // Monster will move towards the player if not in attack range.
                    _owner.stateMachine.ChangeState(MoveState.Instance);
                }
            
            }
        }

        bool PlayerInLOS(MinotaurAI _owner)
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
}

