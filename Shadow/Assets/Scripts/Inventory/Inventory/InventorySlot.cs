using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour  {

	public Image icon;
	public Button itemButton;
	public TMP_Text stackText;

	public Item item;  // Current item in the slot

	// Add item to the slot
	public void AddItem (Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		itemButton.interactable = true;
		if (item.currentAmt > 1)
        {
			stackText.text = "" + item.currentAmt;
        }
		else
        {
			stackText.text = "";
		}
	}

	// Clear the slot
	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		itemButton.interactable = false;
		stackText.text = "";
	}

	public void SelectItem ()
    {
		GetComponentInParent<InventoryUI>().SelectItem(this);
	}

}
