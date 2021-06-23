using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSkills : Skills
{
    public Animator animator;

    public override void NormalAttack()
    {
        animator.SetTrigger("Attack");
    }

    public override void UltimateAttack()
    {
        animator.SetTrigger("UltimateAttack");
    }
}
