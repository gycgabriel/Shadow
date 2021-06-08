using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Parent class of all living breathing things with HP and MP. 
 * Parent class of Player and Enemy.
 */
public class Creature : MonoBehaviour
{
    public Stats stats;
    public string[] skills;
    public Dictionary<string, StatModifier> statModifiers = new Dictionary<string, StatModifier>();          // equipment_name or debuff/buff name as string

    public int currentHP;
    public int currentMP;
    public int currentLevel;
    public bool isDead;

    public Dictionary<string, int> getStats()
    {
        return stats.getStats(statModifiers);
    }

    private void Update()
    {
        if (currentHP <= 0)
        {
            isDead = true;
        } else
        {
            isDead = false;
        }
    }

}
