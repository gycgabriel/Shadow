using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/**
 * Manage which enemy is targeted and to display status of
 */
public class TargetEnemyUIManager : Singleton<TargetEnemyUIManager>
{
    private Player player;
    public TargetEnemyUIDisplay display;       // The enemy status display
    public Enemy targetedEnemy;                // The targeted enemy
    public List<Enemy> alertedEnemies;         // List of enemies that are alerted to the player

    void Start()
    {
        targetedEnemy = null;
        alertedEnemies = new List<Enemy>();

        // Add an method to be executed for whenever a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Whenever loading into a new scene, reset the alerted and targeted enemies
        targetedEnemy = null;
        alertedEnemies = new List<Enemy>();
    }

    private void Update()
    {
        player = PartyController.activePC.gameObject.GetComponent<Player>();

        // If there are no alerted enemies, deactivate the enemy display
        if (alertedEnemies.Count == 0)
        {
            display.gameObject.SetActive(false);
            return;
        }

        // Display the closest alerted enemy's status
        targetedEnemy = findClosestEnemy();

        display.gameObject.SetActive(true);
        display.SetTargetedEnemy(this.targetedEnemy);
    }

    /**
     * Method to iterate through and find the closest alerted enemy.
     */
    Enemy findClosestEnemy()
    {
        Vector2 playerPos = player.transform.position;

        Enemy closestEnemy = alertedEnemies[0];
        float currentShortestDist = Vector2.Distance(closestEnemy.transform.position, playerPos);
        float currentEnemyDist;

        foreach (Enemy enemy in alertedEnemies)
        {
            currentEnemyDist = Vector2.Distance(enemy.transform.position, playerPos);
            if (currentEnemyDist < currentShortestDist)
            {
                currentShortestDist = currentEnemyDist;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public void addAlertedEnemy(Enemy enemy)
    {
        alertedEnemies.Add(enemy);
    }

    public void removeAlertedEnemy(Enemy enemy)
    {
        alertedEnemies.Remove(enemy);
    }

}
