using UnityEngine;
using StateStuff;

namespace MinotaurStates
{
    // Does nothing while waiting for player's dialogue
    public class UnalertState : State<MinotaurAI>
    {
        private static UnalertState _instance;

        private UnalertState()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
        }

        public static UnalertState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new UnalertState();
                }

                return _instance;
            }
        }

        public override void EnterState(MinotaurAI _owner)
        {
        }

        public override void ExitState(MinotaurAI _owner)
        {
        }

        public override void UpdateState(MinotaurAI _owner)
        {
        }
    }
}

