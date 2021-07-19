using UnityEngine;

[CreateAssetMenu(fileName = "New StatPotion", menuName = "Inventory/Consumable/StatPotion")]
public class StatPotion : Consumable
{
    public StatTypes statToBoost;
    public int statBoostAmt;

    public override void Use()
    {
        base.Use();
        PartyController.playerP.stats.addBaseStat(GetStatName(statToBoost), statBoostAmt);
        PartyController.shadowP.stats.addBaseStat(GetStatName(statToBoost), statBoostAmt);
    }

    public string GetStatName(StatTypes type)
    {
        return type switch
        {
            StatTypes.HP => "hp",
            StatTypes.MP => "mp",
            StatTypes.ATK => "atk",
            StatTypes.DEF => "def",
            StatTypes.MATK => "matk",
            StatTypes.MDEF => "mdef",
            _ => null,
        };
    }
}

public enum StatTypes { HP, MP, ATK, DEF, MATK, MDEF }