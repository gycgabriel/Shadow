using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Stores information on the Enemy stats, skills, status, exp gained from killing it.
 */
public class Enemy : Creature
{
    public EnemyInfo enemyInfo;
    public LootTable lootTable;

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
            PartyController.AddGold(lootTable.GetGoldDrop());
            DropLoot();
            this.enabled = false;
        }
        else
        {
            isDead = false;
        }
    }

    void DropLoot()
    {
        Loot lootDropped = lootTable.GetLoot();
        if (lootDropped.item != null)
        {
            ItemPickup spawnedItem = Instantiate(lootDropped.item, RoundToNearestGrid(transform.position), Quaternion.identity);
            spawnedItem.itemAmt = lootDropped.Quantity;
        }
    }

    Vector3 RoundToNearestGrid(Vector3 position)
    {
        Vector3 newPosition = new Vector3
        {
            x = (float)Mathf.Round(position.x - 0.5f) + 0.5f,
            y = (float)Mathf.Round(position.y - 0.5f) + 0.5f
        };
        return newPosition;
    }

}
