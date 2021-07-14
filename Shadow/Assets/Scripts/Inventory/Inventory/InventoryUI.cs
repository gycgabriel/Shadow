using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;  // The entire UI

	public ItemOptions itemOptionsWindow;
	public SelectedItemDisplay selectedItemDisplay;

	public static Item selectedItem;

	Inventory inventory;	// Our current inventory

	InventorySlot[] slots;	// List of all the slots

	void Start () {
		inventory = PartyController.inventory;
		inventory.onItemChangedCallback += UpdateUI;

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

    void OnEnable()
    {
		if (inventory == null)
        {
			inventory = PartyController.inventory;
		}
		if (slots == null)
        {
			slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		}
		itemOptionsWindow.gameObject.SetActive(false);
		UpdateUI();
    }

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	//		- Updating selected item
	// This is called using a delegate on the Inventory.
	void UpdateUI ()
	{
		Debug.Log("Updating Inventory UI.");
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)	// If there is an item to add
			{
				slots[i].AddItem(inventory.items[i]);	// Add it
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		}

		if (selectedItem != null && selectedItem.currentAmt <= 0)
        {
			selectedItem = null;
        }
		selectedItemDisplay.UpdateUI();
	}

	public void SelectItem(InventorySlot slot)
    {
		itemOptionsWindow.gameObject.SetActive(false);
		selectedItem = slot.item;
		selectedItemDisplay.UpdateUI();
	}
}
