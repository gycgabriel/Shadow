using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * Manage the HP Bar display of the current enemy targeted by the player
 */
public class BossUIDisplay : TargetEnemyUIDisplay
{
    public int numOfHPBars;
    public Slider[] hpBars;                    // The Enemy's HP bar

    protected override void Update()
    {
        if (targetedEnemy == null)
        {
            gameObject.SetActive(false);
        }
        Dictionary<string, int> enemyStats = targetedEnemy.getStats();

        int hpPerBar = enemyStats["hp"] / numOfHPBars;

        // Update the Enemy's current HP 
        for (int i = 0; i < numOfHPBars; i++)
        {
            hpBars[i].maxValue = hpPerBar;
            // hp for each bar must be between 0 and hpPerBar
            // E.g. for 1st bar, i = 0, (numOfHPBars - i - 1) = (3 - 0 - 1) = 2, so hp for 1st bar = currentHP - 2 * hpPerBar 
            hpBars[i].value = Mathf.Min(hpPerBar, Mathf.Max(0, targetedEnemy.currentHP - hpPerBar * (numOfHPBars - i - 1)));
        }

        // Update the Enemy's level
        levelText.text = "" + targetedEnemy.currentLevel;

        // Update the Enemy's name
        nameText.text = targetedEnemy.displayName;
    }
}
