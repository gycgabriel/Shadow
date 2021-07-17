using UnityEngine;
using System.Collections;

// HurtBehavior for enemies
public class EnemyHurt : HurtBehaviour
{
    private EnemyAI enemyAI;
    public LootTable lootTable;

    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
    }

    protected override void Update()
    {
        if (creature.isDead)
        {
            DropLoot();
            Destroy(gameObject);
        }
    }

    public override bool Hurt(int damageToGive)
    {
        enemyAI.AlertOn();
        return base.Hurt(damageToGive);
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
