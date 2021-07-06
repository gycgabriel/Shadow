using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores information on the Enemy stats, skills, status, exp gained from killing it.
 */
public class Enemy : Creature
{
    public EnemyInfo enemyInfo;

    [System.NonSerialized]
    public string displayName;

    private void Start()
    {
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
            PartyController.AddExperience(enemyInfo.expReward);
            PartyController.EnemyKilled(enemyInfo.name);
        }
        else
        {
            isDead = false;
        }
    }

}
