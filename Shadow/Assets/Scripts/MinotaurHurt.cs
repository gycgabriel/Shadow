using UnityEngine;

public class MinotaurHurt : HurtBehaviour
{
    private MinotaurAI minotaurAI;

    public bool enteringNewPhase;

    protected override void Start()
    {
        base.Start();
        minotaurAI = GetComponent<MinotaurAI>();
    }

    protected override void Update()
    {
        if (creature.isDead)
        {
            minotaurAI.Die();
            this.enabled = false;
        }
    }

    public override bool Hurt(int damageToGive)
    {
        // if the unit has invincibility frames and is invincible now, no damage will be taken
        if (hasInvincibility && isInvincible || enteringNewPhase || creature.isDead)
        {
            return false;
        }

        creature.currentHP = Mathf.Max(creature.currentHP - damageToGive, 0);        // creature health will not fall below zero

        if (creature.currentHP > 0)
        {
            CheckPhase();
            StartCoroutine(HurtEffect());
        }
        
        return true;
    }

    public void CheckPhase()
    {
        if (minotaurAI.CheckForNextPhase())
        {
            // HP will not go any lower until next phase is entered
            creature.currentHP = creature.getStats()["hp"] * (3 - minotaurAI.currentPhase) / 3;

            // Minotaur is invincible during phase change
            enteringNewPhase = true;
        }
    }

}
