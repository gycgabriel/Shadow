using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;  // The entire UI
	public TMP_Text goldText;

	public ItemOptions itemOptionsWindow;
	public AmtConfirmWindow amtConfirmWindow;
	public SelectHotkeyWindow selectHotkeyWindow;
	public SelectedItemDisplay selectedItemDisplay;

	public Button backButton;

	public static Item selectedItem;

	protected Inventory inventory;	// Our current inventory

	protected InventorySlot[] slots;  // List of all the slots


	void Start () {
		inventory = PartyController.inventory;
		inventory.onItemChangedCallback += UpdateUI;

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

    void OnEnable()
    {
		if (inventory == null || inventory != PartyController.inventory)
        {
			inventory = PartyController.inventory;
			inventory.onItemChangedCallback += UpdateUI;
		}
		if (slots == null)
        {
			slots = itemsParent.GetComponentsInChildren<InventorySlot>();
			SetButtonNavigation();
		}

		// if there are any open windows, set it to inactive
		if (itemOptionsWindow != null)
			itemOptionsWindow.gameObject.SetActive(false);
		if (amtConfirmWindow != null)
			amtConfirmWindow.gameObject.SetActive(false);
		if (selectHotkeyWindow != null)
			selectHotkeyWindow.gameObject.SetActive(false);

		UpdateUI();

		PauseMenu.scriptInstance.SelectButton(slots[0].GetComponentInChildren<Button>());
		slots[0].SelectItem();

	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	//		- Updating selected item
	// This is called using a delegate on the Inventory.
	protected virtual void UpdateUI ()
	{
		// Debug.Log("Updating Inventory UI.");
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

		goldText.text = "" + inventory.Gold;
	}

	public void SelectItem(InventorySlot slot)
    {
		// if there is itemOptionsWindow, set it to inactive
		if (itemOptionsWindow != null)
		{
			itemOptionsWindow.gameObject.SetActive(false);
		}

		selectedItem = slot.item;
		selectedItemDisplay.UpdateUI();
	}


	private void SetButtonNavigation()
    {
		int totalSlots = 24;
		int slotsPerRow = 8;

		Button[] slotButtons = new Button[totalSlots];

		for (int i = 0; i < totalSlots; i++)
        {
			slotButtons[i] = slots[i].gameObject.GetComponentInChildren<Button>();
        }

		// Set Navigation of first slot to link to back button
		Navigation navi = slotButtons[0].navigation;
		navi.mode = Navigation.Mode.Explicit;
		navi.selectOnUp = backButton;
		navi.selectOnLeft = backButton;
		navi.selectOnRight = slotButtons[1];
		navi.selectOnDown = slotButtons[slotsPerRow];
		slotButtons[0].navigation = navi;

		// Set Navigation for the rest of the buttons
		for (int i = 1; i < totalSlots; i++)
        {
			navi = slotButtons[i].navigation;
			navi.mode = Navigation.Mode.Explicit;

			if (i % slotsPerRow != 0)
				navi.selectOnLeft = slotButtons[i - 1];

			if (i % slotsPerRow != 7)
				navi.selectOnRight = slotButtons[i + 1];

			if (i >= slotsPerRow)
				navi.selectOnUp = slotButtons[i - slotsPerRow];

			if (i < 2 * slotsPerRow)
				navi.selectOnDown = slotButtons[i + slotsPerRow];

			slotButtons[i].navigation = navi;
		}
    }
}
