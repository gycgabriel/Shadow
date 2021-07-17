using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Adapted from https://medium.com/@guilhermegm/unity-simple-loot-system-d181220e6542

[System.Serializable]
public class Loot
{
    public ItemPickup item;
    public int lots;
    public int minQuantity;
    public int maxQuantity;

    public int Quantity { get; set; }
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public int minGoldDrop;
    public int maxGoldDrop;

    public Loot[] loots;

    public Loot GetLoot()
    {
        // Determine what loot drops by using a lottery system,
        // with each loot in the table having a certain number of lots.
        int totalLots = 0;
        foreach (Loot loot in loots)
        {
            totalLots += loot.lots;
        }

        int drawnLot = Random.Range(1, totalLots + 1);
        Loot lootDropped = null;

        foreach (Loot loot in loots)
        {
            if (drawnLot <= loot.lots)
            {
                loot.Quantity = RandomQuantity(loot);
                lootDropped = loot;
                break;
            }
            else
            {
                drawnLot -= loot.lots;
            }
        }

        return lootDropped;
    }

    public int GetGoldDrop()
    {
        return Random.Range(minGoldDrop, maxGoldDrop + 1);
    }

    public int RandomQuantity(Loot loot)
    {
        return Random.Range(loot.minQuantity, loot.maxQuantity);
    }
}
