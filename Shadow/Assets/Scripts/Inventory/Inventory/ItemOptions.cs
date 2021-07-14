using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOptions : MonoBehaviour
{
	public DiscardWindow discardWindow;

	// Called when the remove button is pressed
	public void OnRemoveButton()
	{
		discardWindow.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}

	// Called when the item is pressed
	public void UseItem()
	{
		InventoryUI.selectedItem.Use();
		InventoryUI.selectedItem.RemoveFromInventory(1);
		gameObject.SetActive(false);
	}
}
