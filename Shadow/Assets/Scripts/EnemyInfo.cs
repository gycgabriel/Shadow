using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Template for storing information for the different types of enemies.
 */
[CreateAssetMenu(fileName = "New EnemyInfo", menuName = "EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public new string name;
    public int level;
    public int expReward;

    public int hp;
    public int mp;
    public int atk;
    public int def;
    public int matk;
    public int mdef;
    public int agi;
    public int luk;

    public Stats getStats()
    {
        return new Stats(hp, mp, atk, def, matk, mdef, agi, luk);
    }
}
