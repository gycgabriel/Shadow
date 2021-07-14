using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	// Callback which is triggered when
	// an item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 24;	// Amount of slots in inventory

	// Current list of items in inventory
	public List<Item> items = new List<Item>();

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
        }
		else
        {
			items.Add(MonoBehaviour.Instantiate(item));    // Add a clone of the item to list
        }
		
		/*
		Debug.Log("Current Items:");
		foreach (Item itemInInventory in items)
        {
			Debug.Log("    " + itemInInventory.name + ": " + itemInInventory.currentAmt);
		}
		*/

		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();

		return true;
	}

	// Remove an item
	public void Remove (Item item, bool toDestroy)
	{
		Item itemInInventory = items.Find(x => x == item);
		items.Remove(itemInInventory);

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
}
