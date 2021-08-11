using UnityEngine;
using StateStuff;

namespace MinotaurStates
{
    public class DeathState : State<MinotaurAI>
    {
        private static DeathState _instance;

        private DeathState()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static DeathState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new DeathState();
                }

                return _instance;
            }
        }

        public override void EnterState(MinotaurAI _owner)
        {
            // Debug.Log("Entering Minotaur Idle State");
            _owner.anim.Play("Base Layer.Minotaur_Die", 0, 0f);
        }

        public override void ExitState(MinotaurAI _owner)
        {
            // Debug.Log("Exiting Minotaur Idle State");
        }

        public override void UpdateState(MinotaurAI _owner)
        {
            
        }
    }
}