using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurHurt : HurtBehaviour
{
    private MinotaurAI minotaurAI;

    protected override void Start()
    {
        base.Start();
        minotaurAI = GetComponent<MinotaurAI>();
    }

    protected override void Update()
    {
        if (creature.isDead)
        {
            Destroy(gameObject);
        }
    }

    public override bool Hurt(int damageToGive)
    {
        // if the unit has invincibility frames and is invincible now, no damage will be taken
        if (hasInvincibility && isInvincible)
        {
            return false;
        }

        creature.currentHP = Mathf.Max(creature.currentHP - damageToGive, 0);        // creature health will not fall below zero

        CheckPhase();

        StartCoroutine(HurtEffect());
        return true;
    }

    public void CheckPhase()
    {
        if (minotaurAI.CheckForNextPhase())
        {
            // HP will not go any lower until next phase is entered
            creature.currentHP = creature.getStats()["hp"] * (3 - minotaurAI.currentPhase) / 3;

            // Minotaur is invincible during phase change
            hasInvincibility = true;
            isInvincible = true;
        }
    }

}
