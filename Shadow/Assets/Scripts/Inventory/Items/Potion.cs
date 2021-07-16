using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Consumable/Potion")]
public class Potion : Consumable
{
    public int recoveryAmt;
    public GameObject recoveryEffect;
    
    public override void Use()
    {
        base.Use();
        if (consumableType == ConsumableType.HealthPotion)
        {
            PartyController.activePC.GetComponent<PlayerHurt>().RecoverHP(recoveryAmt);
        }
        else if (consumableType == ConsumableType.ManaPotion)
        {
            PartyController.activePC.GetComponent<PlayerHurt>().RecoverMP(recoveryAmt);
        }
    }
}
