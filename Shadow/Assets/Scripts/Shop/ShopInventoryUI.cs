using System.Collections.Generic;

public class ShopInventoryUI : InventoryUI
{
	public List<Item> shopItemList;

	protected override void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < shopItemList.Count)
			{
				slots[i].AddItem(shopItemList[i]);
			}
			else
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
}
