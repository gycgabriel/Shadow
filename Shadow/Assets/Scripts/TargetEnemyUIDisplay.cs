using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * Manage the HP Bar display of the current enemy targeted by the player
 */
public class TargetEnemyUIDisplay : MonoBehaviour
{
    private Enemy targetedEnemy;            // The Enemy

    public Slider hpBar;                    // The Enemy's HP bar

    public TMP_Text nameText;               // The Text object displaying the Enemy's name
    public TMP_Text levelText;              // The Text object displaying the Enemy's level

    void Update()
    {
        Dictionary<string, int> enemyStats = targetedEnemy.getStats();

        // Update the Enemy's current HP 
        hpBar.maxValue = enemyStats["hp"];
        hpBar.value = targetedEnemy.currentHP;

        // Update the Enemy's level
        levelText.text = "" + targetedEnemy.currentLevel;

        // Update the Enemy's name
        nameText.text = targetedEnemy.displayName;
    }

    public void setTargetedEnemy(Enemy enemy)
    {
        targetedEnemy = enemy;
    }
}
