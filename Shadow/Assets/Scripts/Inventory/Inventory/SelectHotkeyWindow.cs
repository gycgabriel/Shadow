using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHotkeyWindow : MonoBehaviour
{
	public Transform SelectHotKeyPanel;
	InventorySlot[] slots;  // List of all the slots

	void Start()
	{
		// Populate our slots array
		slots = SelectHotKeyPanel.GetComponentsInChildren<InventorySlot>();
	}

    private void OnEnable()
    {
		Debug.Log("Updating Select Hotkey UI.");
		if (slots == null)
        {
			slots = SelectHotKeyPanel.GetComponentsInChildren<InventorySlot>();
		}

		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (ItemHotkeyUIManager.scriptInstance.hotkeyItems[i] != null)  // If there is an item to add
			{
				slots[i].AddItem(ItemHotkeyUIManager.scriptInstance.hotkeyItems[i]);   // Add it
			}
			else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
				slots[i].itemButton.interactable = true;
			}
		}
	}

	public void SelectHotkeySlot(int hotkeyNum)
    {
		ItemHotkeyUIManager.scriptInstance.SetHotkeyItem(hotkeyNum, (Consumable)InventoryUI.selectedItem);
		gameObject.SetActive(false);
		PauseMenu.scriptInstance.PopInfoWindow("New Hotkey Set!");
    }

	public void Return()
    {
		gameObject.SetActive(false);
    }
}
