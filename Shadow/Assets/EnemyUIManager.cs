using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Manage the HP bar display for enemies.
 */
public class EnemyUIManager : MonoBehaviour
{
    private Enemy theEnemy;

    public Slider hpBar;
    public TMP_Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        theEnemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Dictionary<string, int> enemyStats = theEnemy.getBaseStats();

        // Update the enemy's current HP 
        hpBar.maxValue = enemyStats["hp"];
        hpBar.value = theEnemy.currentHP;

        // Update the enemy's current level
        levelText.text = "Lvl " + theEnemy.currentLevel;
    }
}
