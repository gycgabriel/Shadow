using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	// Callback which is triggered when
	// an item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 24;  // Amount of slots in inventory

	public int Gold { get; set; }

	public List<Item> items = new List<Item>(); // Current list of items in inventory

	// Add a new item. If there is enough room we
	// return true. Else we return false.
	public bool Add (Item item)
	{
		// Check if out of space
		if (items.Count >= space)
		{
			Debug.Log("Not enough room.");
			return false;
		}

		if (item.isStackable && items.Find(x => x.name.Equals(item.name)) != null) {
			Item itemInInventory = items.Find(x => x.name.Equals(item.name));
			itemInInventory.currentAmt += item.currentAmt;

			PartyController.ItemGet(item.name, itemInInventory.currentAmt);
		}
		else
        {
			items.Add(MonoBehaviour.Instantiate(item));    // Add a clone of the item to list
			items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber)); // Sort the items

			PartyController.ItemGet(item.name, 1);
		}

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();

		
		// Quest: Check if fulfil gathering quest requirements
		Debug.Log("Item: " + item.name + " x " + GetItem(item.itemNumber)?.currentAmt);

		return true;
	}

	// Remove an item
	public void Remove (Item item, bool toDestroy)
	{
		Item itemInInventory = items.Find(x => x == item);
		items.Remove(itemInInventory);
		items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber));

		if (toDestroy)
        {
			MonoBehaviour.Destroy(itemInInventory);
        }

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public void Remove (Item item, int amt)
	{
		Item itemInInventory = items.Find(x => x == item);
		itemInInventory.currentAmt -= amt;

		if (itemInInventory.currentAmt <= 0)
        {
			items.Remove(itemInInventory);
			MonoBehaviour.Destroy(itemInInventory);
			items.Sort((x1, x2) => x1.itemNumber.CompareTo(x2.itemNumber));
		}

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public int GetItemAmt (Item item)
    {
		if (items.Find(x => x == item) == null)
        {
			return 0;
        }
		else
        {
			return items.Find(x => x == item).currentAmt;
		}
	}

	public Item GetItem (int itemNumber)
    {
		return items.Find(x => x.itemNumber == itemNumber);
    }

	public void ClearInventory()
    {
		items.Clear();
    }

	public int[,] SaveInventory()
    {
		int[,] inventory = new int[items.Count,2];
		for (int i = 0; i < items.Count; i++)
        {
			inventory[i, 0] = items[i].itemNumber;
			inventory[i, 1] = items[i].currentAmt;
		}

		return inventory;
    }
}
