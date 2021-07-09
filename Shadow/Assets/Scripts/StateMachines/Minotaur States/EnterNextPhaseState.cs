using UnityEngine;
using StateStuff;
using System.Collections;

namespace MinotaurStates
{
    public class EnterNextPhaseState : State<MinotaurAI>
    {
        private static EnterNextPhaseState _instance;

        private EnterNextPhaseState()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static EnterNextPhaseState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new EnterNextPhaseState();
                }

                return _instance;
            }
        }

        public override void EnterState(MinotaurAI _owner)
        {
            _owner.currentPhase++;
            Debug.Log("Entering Phase " + _owner.currentPhase);

            // Change of phase, attack pattern and attack frequency
            _owner.attackPattern = _owner.attackPatterns[_owner.currentPhase - 1].attackPattern;
            _owner.attackPatternCounter = 0;
            _owner.timeBetweenMove = _owner.timeBetweenMoveArray[_owner.currentPhase - 1];

            _owner.moveDirection = _owner.player.transform.position - _owner.transform.position;
            _owner.anim.SetFloat("LastMoveX", _owner.moveDirection.x);
            _owner.anim.SetFloat("LastMoveY", _owner.moveDirection.y);

            _owner.anim.SetTrigger("enterNextPhase");
        }

        public override void ExitState(MinotaurAI _owner)
        {
            // Debug.Log("Exiting Attack State");
            _owner.anim.SetTrigger("stopAttack");
            _owner.GetComponent<MinotaurHurt>().hasInvincibility = false;
            _owner.GetComponent<MinotaurHurt>().isInvincible = false;
        }

        public override void UpdateState(MinotaurAI _owner)
        {

        }
    }
}
