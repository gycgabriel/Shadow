using UnityEngine;
using StateStuff;

namespace MinotaurStates
{
    public class MoveState : State<MinotaurAI>
    {
        private static MoveState _instance;

        private Vector2[] moveDirections = new Vector2[] //The four possible move directions
        {
        new Vector2(0f,1f),
        new Vector2(1f,0f),
        new Vector2(0f,-1f),
        new Vector2(-1f,0f)
        };

        private MoveState()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static MoveState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new MoveState();
                }

                return _instance;
            }
        }

        public override void EnterState(MinotaurAI _owner)
        {
            // Debug.Log("Entering Move State");
            _owner.anim.SetTrigger("move");
            if (_owner.isAlert)
            {
                AlertMove(_owner);
            }
            else
            {
                NonAlertMove(_owner);
            }
        }

        public override void ExitState(MinotaurAI _owner)
        {
            // Debug.Log("Exiting Move State");
            _owner.anim.SetTrigger("stopMove");
        }

        public override void UpdateState(MinotaurAI _owner)
        {
            Vector3 currentPos = _owner.transform.position;
            float sqrRemainingDistance = (currentPos - _owner.dest).sqrMagnitude;

            //While that distance is greater than a very small amount (Epsilon, almost zero):
            if (sqrRemainingDistance > float.Epsilon)
            {
                _owner.transform.position = Vector3.MoveTowards(currentPos, _owner.dest, _owner.moveSpeed * Time.deltaTime);
            }
            else
            {
                _owner.stateMachine.ChangeState(IdleState.Instance);

            }
        }

        /** 
         * Checks if possible to move to movePoint.
         */
        private bool CanMove(MinotaurAI _owner, Vector2 moveDirection)
        {
            Vector2 start = _owner.transform.position;
            Vector2 dest = (Vector2)_owner.transform.position + moveDirection;

            _owner.monsterGridCollider.enabled = false;                                         // linecast doesn't hit this object's own collider

            // Create linecast from player to the middle and the four edges of the intended move point
            RaycastHit2D[] hit = new RaycastHit2D[5];
            hit[0] = Physics2D.Linecast(start, dest, _owner.blockingLayer);                            // Center
            hit[1] = Physics2D.Linecast(start, dest + new Vector2(0f, 0.495f), _owner.blockingLayer);    // Top edge
            hit[2] = Physics2D.Linecast(start, dest + new Vector2(0f, -0.495f), _owner.blockingLayer);   // Bottom edge
            hit[3] = Physics2D.Linecast(start, dest + new Vector2(0.495f, 0f), _owner.blockingLayer);    // Right edge
            hit[4] = Physics2D.Linecast(start, dest + new Vector2(-0.495f, 0f), _owner.blockingLayer);   // Left edge

            _owner.monsterGridCollider.enabled = true;

            bool canMove = true;
            for (int i = 0; i < 5; i++)
            {
                if (hit[i].transform != null)
                {
                    // Obstacle detected, unable to move to destination
                    Debug.Log("Slime collided with " + hit[i].collider.name);
                    canMove = false;
                    break;
                }
            }

            return canMove;
        }

        private void NonAlertMove(MinotaurAI _owner)
        {
            //Randomly select a direction for the slime to move
            Vector2 moveDirection = moveDirections[Random.Range(0, 4)];

            _owner.moveDirection = moveDirection;
            _owner.anim.SetFloat("LastMoveX", moveDirection.x);
            _owner.anim.SetFloat("LastMoveY", moveDirection.y);

            if (CanMove(_owner, moveDirection))
            {
                _owner.dest = _owner.transform.position + (Vector3)moveDirection;
            }
            else
            {
                _owner.stateMachine.ChangeState(IdleState.Instance);
            }
        }

        private void AlertMove(MinotaurAI _owner)
        {
            Vector2 moveDirection = _owner.player.transform.position - _owner.transform.position;

            _owner.moveDirection = moveDirection;
            _owner.anim.SetFloat("LastMoveX", moveDirection.x);
            _owner.anim.SetFloat("LastMoveY", moveDirection.y);

            // Attempt to move horizontally towards the player first. if unable to, then move vertically.
            if (moveDirection.x > 0f && CanMove(_owner, new Vector2(1, 0)))
            {
                _owner.dest = _owner.transform.position + new Vector3(1, 0, 0);
            }
            else if (moveDirection.x < 0f && CanMove(_owner, new Vector2(-1, 0)))
            {
                _owner.dest = _owner.transform.position + new Vector3(-1, 0, 0);
            }
            else if (moveDirection.y > 0f && CanMove(_owner, new Vector2(0, 1)))
            {
                _owner.dest = _owner.transform.position + new Vector3(0, 1, 0);
            }
            else if (moveDirection.y < 0f && CanMove(_owner, new Vector2(0, -1)))
            {
                _owner.dest = _owner.transform.position + new Vector3(0, -1, 0);
            }
            else
            {
                _owner.stateMachine.ChangeState(IdleState.Instance);
            }
        }
    }
}
