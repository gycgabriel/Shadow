using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererSkills : Skills
{
    public Animator animator;
    public Transform spellFirePoint;          // The point where the fireball will be generated at    
    public Fireball fireballPrefab;           // The fireball object to be launched
    public GameObject manaBurstPrefab;

    public override void NormalAttack()
    {
        animator.SetTrigger("Attack");
    }

    public override void UltimateAttack()
    {
        animator.SetTrigger("UltimateAttack");
    }

    /**
     * Coroutine to summon fireball, Sorcerer's normal attack
     */
    public void CastFireball()
    {
        Instantiate(fireballPrefab, spellFirePoint.position, spellFirePoint.rotation);
    }

    /**
     * Coroutine to summon Mana Burst, Sorcerer's ultimate attack
     */
    public void CastManaBurst()
    {
        Instantiate(manaBurstPrefab, spellFirePoint.position, spellFirePoint.rotation);
    }
}
