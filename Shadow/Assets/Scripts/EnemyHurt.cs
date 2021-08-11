using UnityEngine;
using System.Collections;

// HurtBehavior for enemies
public class EnemyHurt : HurtBehaviour
{
    private EnemyAI enemyAI;

    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
    }

    protected override void Update()
    {
        if (creature.isDead)
        {
            enemyAI.Die();
            this.enabled = false;
        }
    }

    public override bool Hurt(int damageToGive)
    {
        enemyAI.AlertOn();
        return base.Hurt(damageToGive);
    }
}
