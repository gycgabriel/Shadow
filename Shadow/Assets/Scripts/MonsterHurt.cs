using UnityEngine;
using System.Collections;

public class MonsterHurt : HurtBehaviour
{
    private MonsterAI monsterAI;

    protected override void Start()
    {
        base.Start();
        monsterAI = GetComponent<MonsterAI>();
    }

    public override bool hurt(int damageToGive)
    {
        monsterAI.AlertOn();
        return base.hurt(damageToGive);
    }
}
