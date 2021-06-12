using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores information on the Enemy stats, skills, status, exp gained from killing it.
 */
public class Enemy : Creature
{
    public EnemyInfo enemyInfo;

    public Player thePlayer;

    public string displayName;

    private void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        stats = enemyInfo.getStats();
        currentHP = enemyInfo.hp;
        currentMP = enemyInfo.mp;
        currentLevel = enemyInfo.level;
        displayName = enemyInfo.name;
    }

    private void Update()
    {
        if (currentHP <= 0)
        {
            isDead = true;
            thePlayer.addExperience(enemyInfo.expReward);
        }
        else
        {
            isDead = false;
        }
    }

}
