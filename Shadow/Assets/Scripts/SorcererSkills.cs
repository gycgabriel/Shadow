using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererSkills : MonoBehaviour
{
    public Transform spellFirePoint;          // The point where the fireball will be generated at    
    public Fireball fireballPrefab;           // The fireball object to be launched
    
    /**
     * Coroutine to summon fireball, Sorcerer's normal attack
     */
    public void CastFireball()
    {
        Instantiate(fireballPrefab, spellFirePoint.position, spellFirePoint.rotation);
    }

}
