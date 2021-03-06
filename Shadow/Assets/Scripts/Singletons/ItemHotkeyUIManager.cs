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
     * Checks if hotkeyed item is on cooldown. if no hotkeyed item, return true as well.
     */
    public bool IsHotkeyItemOnCooldown(int hotkeyNum)
    {
        if (hotkeyItems[hotkeyNum] == null)
            return true;
        else
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

    public void UseHotkeyItem(int hotkeyNum)
    {
        UseItem(hotkeyItems[hotkeyNum]);
    }

    public void UseItem(Consumable item)
    {
        item.Use();
        item.RemoveFromInventory(1);

        ConsumableType itemType = item.consumableType;

        isConsumableOnCooldown[itemType] = true;

        // Update cooldown time counter for the used item's consumable type
        consumableCDCounters[itemType] = Consumable.GetConsumableTypeCD(itemType);

        // Update hotkey item cooldown images
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

    public int[] SaveHotkeys()
    {
        int[] hotkeys = new int[NumOfHotkeys];
        for (int i = 0; i < NumOfHotkeys; i++)
        {
            if (hotkeyItems[i] == null)
                hotkeys[i] = 0;
            else
                hotkeys[i] = hotkeyItems[i].itemNumber;
        }

        return hotkeys;
    }

    public float[] SaveHotkeyCooldown()
    {
        float[] hotkeyCooldown = new float[consumableCDCounters.Count];
        consumableCDCounters.Values.CopyTo(hotkeyCooldown, 0);
        return hotkeyCooldown;
    }

    public bool[] SaveIsHotkeyOnCooldown()
    {
        bool[] isHotkeyOnCooldown = new bool[isConsumableOnCooldown.Count];
        isConsumableOnCooldown.Values.CopyTo(isHotkeyOnCooldown, 0);
        return isHotkeyOnCooldown;
    }

    public void LoadHotkeys(int[] hotkeys)
    {
        hotkeyItems = new Consumable[NumOfHotkeys];
        for (int i = 0; i < NumOfHotkeys; i++)
        {
            hotkeyItems[i] = (Consumable) PartyController.inventory.GetItem(hotkeys[i]);
        }
    }

    public void LoadHotkeyCooldown(float[] hotkeyCooldown) 
    {
        consumableCDCounters = new Dictionary<ConsumableType, float>();
        ConsumableType[] types = (ConsumableType[])System.Enum.GetValues(typeof(ConsumableType));
        for (int i = 0; i < types.Length; i++)
        {
            consumableCDCounters.Add(types[i], hotkeyCooldown[i]);
        }
    }

    public void LoadIsHotkeyOnCooldown(bool[] isHotkeyOnCooldown)
    {
        isConsumableOnCooldown = new Dictionary<ConsumableType, bool>();
        ConsumableType[] types = (ConsumableType[])System.Enum.GetValues(typeof(ConsumableType));
        for (int i = 0; i < types.Length; i++)
        {
            isConsumableOnCooldown.Add(types[i], isHotkeyOnCooldown[i]);
        }
    }
}
