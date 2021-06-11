using UnityEngine;
using System.Collections;

public class EnemyHurt : HurtBehaviour
{
    private EnemyAI monsterAI;

    protected override void Start()
    {
        base.Start();
        monsterAI = GetComponent<EnemyAI>();
    }

    public override bool hurt(int damageToGive)
    {
        monsterAI.AlertOn();
        return base.hurt(damageToGive);
    }
}
