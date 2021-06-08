using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores information on the Enemy stats, skills, status, exp gained from killing it.
 */
public class Enemy : Creature
{
    public int expReward;

    public Player thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (currentHP <= 0)
        {
            isDead = true;
            thePlayer.addExperience(expReward);
        }
        else
        {
            isDead = false;
        }
    }

}
