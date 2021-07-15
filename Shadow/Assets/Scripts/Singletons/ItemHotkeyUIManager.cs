using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHotkeyUIManager : Singleton<ItemHotkeyUIManager>
{
    public const int NumOfHotkeys = 4;

    public Image[] hotkeyItemIcons;
    public TMP_Text[] hotkeyItemStackTexts;
    public Image[] hotkeyItemCDImages;

    public Consumable[] hotkeyItems;
    public Dictionary<ConsumableType, float> consumableCDCounters;
    public Dictionary<ConsumableType, bool> isConsumableOnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        if (hotkeyItems.Length == 0)
        {
            hotkeyItems = new Consumable[NumOfHotkeys];
            Debug.Log("initializing item hotkeys");
        }
        if (consumableCDCounters == null)
        {
            consumableCDCounters = new Dictionary<ConsumableType, float>();
            foreach (ConsumableType type in (ConsumableType[]) System.Enum.GetValues(typeof(ConsumableType)))
            {
                consumableCDCounters.Add(type, 0f);
            }
        }
        if (isConsumableOnCooldown == null)
        {
            isConsumableOnCooldown = new Dictionary<ConsumableType, bool>();
            foreach (ConsumableType type in (ConsumableType[])System.Enum.GetValues(typeof(ConsumableType)))
            {
                isConsumableOnCooldown.Add(type, false);
            }
        }
        Debug.Log("hotkeys.length: " + hotkeyItems.Length);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateConsumableCooldowns();
        UpdateHotkeyIcons();
    }

    public void UseHotkeyItem(int hotkeyNum)
    {
        ConsumableType itemType = hotkeyItems[hotkeyNum].consumableType;

        isConsumableOnCooldown[itemType] = true;

        // Update cooldown time counter for the used item's consumable type
        consumableCDCounters[itemType] = Consumable.GetConsumableTypeCD(itemType);

        // Update skill cooldown image
        UpdateHotkeyIcons();
    }

    // Update Cooldown timer for all consumable types
    private void UpdateConsumableCooldowns()
    {
        foreach (ConsumableType type in (ConsumableType[])System.Enum.GetValues(typeof(ConsumableType)))
        {
            if (isConsumableOnCooldown[type])
            {
                consumableCDCounters[type] -= Time.deltaTime;

                if (consumableCDCounters[type] <= 0)
                {
                    consumableCDCounters[type] = 0;
                    isConsumableOnCooldown[type] = false;
                }
            }
        }
    }
    
    // Update Cooldown display and icon for all hotkey items
    private void UpdateHotkeyIcons()
    {
        for (int i = 0; i < NumOfHotkeys; i++)
        {
            if (hotkeyItems[i] != null)
            {
                hotkeyItemIcons[i].sprite = hotkeyItems[i].icon;
                hotkeyItemIcons[i].enabled = true;
                if (hotkeyItems[i].currentAmt > 1)
                {
                    hotkeyItemStackTexts[i].text = "" + hotkeyItems[i].currentAmt;
                }
                else
                {
                    hotkeyItemStackTexts[i].text = "";
                }

                ConsumableType type = hotkeyItems[i].consumableType;
                hotkeyItemCDImages[i].fillAmount = consumableCDCounters[type] / Consumable.GetConsumableTypeCD(type);
            }
            else
            {
                hotkeyItemIcons[i].sprite = null;
                hotkeyItemStackTexts[i].text = "";
                hotkeyItemIcons[i].enabled = false;
                hotkeyItemCDImages[i].fillAmount = 0;
            }
        }
    }

    /**
     * Checks ultimate skill cooldown. shadowActive determines to check player's or shadow's cooldown.
     */
    public bool IsHotkeyItemOnCooldown(int hotkeyNum)
    {
        return isConsumableOnCooldown[hotkeyItems[hotkeyNum].consumableType];
    }

    public bool IsItemOnCooldown(Consumable item)
    {
        return isConsumableOnCooldown[item.consumableType];
    }

    public float GetRemainingCooldown(Consumable item)
    {
        return consumableCDCounters[item.consumableType];
    }

    public void UseItem(Consumable item)
    {
        ConsumableType itemType = item.consumableType;

        isConsumableOnCooldown[itemType] = true;

        // Update cooldown time counter for the used item's consumable type
        consumableCDCounters[itemType] = Consumable.GetConsumableTypeCD(itemType);

        // Update skill cooldown image
        UpdateHotkeyIcons();
    }

    public void SetHotkeyItem(int hotkeyNum, Consumable item)
    {
        for (int i = 0; i < NumOfHotkeys; i++)
        {
            if (hotkeyItems[i] == item)
            {
                hotkeyItems[i] = null;
                break;
            }
        }
        hotkeyItems[hotkeyNum] = item;
    }
}
