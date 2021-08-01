using UnityEngine;
using UnityEngine.UI;

public class InventoryItemOptions : ItemOptions
{
	public GameObject setHotkeyWindow;
	public GameObject discardWindow;
	public Button useButton;
	public Button setHotkeyButton;
	public Button discardButton;

	private void OnEnable()
	{
		if (InventoryUI.selectedItem == null)
        {
			useButton.interactable = false;
			setHotkeyButton.interactable = false;
			discardButton.interactable = false;
		}
		else
        {
			useButton.interactable = InventoryUI.selectedItem.GetType().IsSubclassOf(typeof(Consumable));
			setHotkeyButton.interactable = InventoryUI.selectedItem.GetType().IsSubclassOf(typeof(Consumable));
			discardButton.interactable = !(InventoryUI.selectedItem.GetType() == (typeof(QuestItem)));
		}
		useButton.Select();
		useButton.OnSelect(null);
	}

    public void UseItem()
	{
		Consumable item = (Consumable) InventoryUI.selectedItem;
		if (!ItemHotkeyUIManager.scriptInstance.IsItemOnCooldown(item))
        {
			ItemHotkeyUIManager.scriptInstance.UseItem(item);
			gameObject.SetActive(false);
			selectedSlotBtn.Select();
			selectedSlotBtn.OnSelect(null);
		}
		else
        {
			float remainingCD = ItemHotkeyUIManager.scriptInstance.GetRemainingCooldown(item);
			string message = string.Format("Item still on cooldown!\n Cooldown: {0:F1}s", remainingCD);
			PauseMenu.scriptInstance.PopInfoWindow(message, selectedSlotBtn);
        }
	}

	public void OnSetHotKeyButton()
    {
		setHotkeyWindow.SetActive(true);
		gameObject.SetActive(false);
	}

    // Called when the remove button is pressed
    public void OnDiscardButton()
	{
		discardWindow.SetActive(true);
		discardWindow.GetComponent<AmtConfirmWindow>().InitialAmt(1);
		gameObject.SetActive(false);
	}
}
