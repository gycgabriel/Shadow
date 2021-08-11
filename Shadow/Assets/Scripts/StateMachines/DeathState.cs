using UnityEngine;
using StateStuff;

public class DeathState : State<EnemyAI>
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

    public override void EnterState(EnemyAI _owner)
    {
        // Debug.Log("Entering Death State");

        _owner.anim.SetTrigger("die");
    }

    public override void ExitState(EnemyAI _owner)
    {
        // Debug.Log("Exiting Death State");
    }

    public override void UpdateState(EnemyAI _owner)
    {

    }
}
