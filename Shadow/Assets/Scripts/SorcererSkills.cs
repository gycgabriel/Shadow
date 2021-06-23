using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererSkills : Skills
{
    public Animator animator;
    public Transform spellFirePoint;          // The point where the fireball will be generated at    
    public Fireball fireballPrefab;           // The fireball object to be launched

    public override void NormalAttack()
    {
        animator.SetTrigger("Attack");
    }

    public override void UltimateAttack()
    {
        Debug.Log("Used Sorcerer's Ultimate Attack");
        FindObjectOfType<PlayerController>().StopAttack();
    }

    /**
     * Coroutine to summon fireball, Sorcerer's normal attack
     */
    public void CastFireball()
    {
        Instantiate(fireballPrefab, spellFirePoint.position, spellFirePoint.rotation);
    }
}
