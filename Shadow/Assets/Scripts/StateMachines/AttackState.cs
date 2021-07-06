using UnityEngine;
using StateStuff;

public class AttackState : State<EnemyAI>
{
    private static AttackState _instance;

    private AttackState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static AttackState Instance
    {
        get
        {
            if (_instance == null)
            {
                new AttackState();
            }

            return _instance;
        }
    }

    public override void EnterState(EnemyAI _owner)
    {
        // Debug.Log("Entering Attack State");

        _owner.moveDirection = _owner.player.transform.position - _owner.transform.position;
        _owner.anim.SetFloat("LastMoveX", _owner.moveDirection.x);
        _owner.anim.SetFloat("LastMoveY", _owner.moveDirection.y);

        _owner.anim.SetTrigger("attack");
    }

    public override void ExitState(EnemyAI _owner)
    {
        // Debug.Log("Exiting Attack State");
        _owner.anim.SetTrigger("stopAttack");
    }

    public override void UpdateState(EnemyAI _owner)
    {

    }
}
