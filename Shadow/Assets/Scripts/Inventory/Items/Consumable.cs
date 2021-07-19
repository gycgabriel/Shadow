using UnityEngine;

public abstract class Consumable : Item
{
    public const float HealthPotionCD = 5f;
    public const float ManaPotionCD = 5f;
    public const float StatPotionCD = 0f;

    public ConsumableType consumableType;
    
    public static float GetConsumableTypeCD(ConsumableType type)
    {
        switch (type)
        {
            case ConsumableType.HealthPotion:
                return HealthPotionCD;
            case ConsumableType.ManaPotion:
                return ManaPotionCD;
            case ConsumableType.StatPotion:
                return StatPotionCD;
            default:
                return -1;  // Error
        }
    }
}

public enum ConsumableType { HealthPotion, ManaPotion, StatPotion }
