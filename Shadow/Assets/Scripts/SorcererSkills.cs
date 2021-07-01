using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererSkills : SkillSet
{
    public Player player;
    public Animator animator;
    public Transform spellFirePoint;          // The point where the fireball will be generated at    
    public GameObject fireballPrefab;           // The fireball object to be launched
    public GameObject manaBurstPrefab;

    public override void NormalAttack()
    {
        animator.SetTrigger("Attack");
    }

    public override void UltimateSkill()
    {
        player.currentMP -= ultimateSkill.skillMPCost;
        animator.SetTrigger("UltimateSkill");
    }

    /**
     * Coroutine to summon fireball, Sorcerer's normal attack
     */
    public void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, spellFirePoint.position, spellFirePoint.rotation);
        fireball.GetComponentInChildren<HurtEnemy>().thePlayer = GetComponentInParent<Player>();
    }

    /**
     * Coroutine to summon Mana Burst, Sorcerer's ultimate attack
     */
    public void CastManaBurst()
    {
        GameObject manaBurst = Instantiate(manaBurstPrefab, spellFirePoint.position, spellFirePoint.rotation);
        manaBurst.GetComponentInChildren<HurtEnemy>().thePlayer = GetComponentInParent<Player>();
    }
}
