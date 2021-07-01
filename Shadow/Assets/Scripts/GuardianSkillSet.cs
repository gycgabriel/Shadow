using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSkillSet : SkillSet
{
    public Player player;
    public Animator animator;

    public override void NormalAttack()
    {
        animator.SetTrigger("Attack");
    }

    public override void UltimateSkill()
    {
        player.currentMP -= ultimateSkill.skillMPCost;
        animator.SetTrigger("UltimateSkill");
    }
}
