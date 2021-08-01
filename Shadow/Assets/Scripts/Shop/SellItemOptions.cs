using UnityEngine;
using UnityEngine.UI;

public class SellItemOptions : ItemOptions
{
	public Button sellButton;

	public GameObject sellWindow;

	private void OnEnable()
	{
		if (InventoryUI.selectedItem == null)
		{
			sellButton.interactable = false;
		}
		else
		{
			sellButton.interactable = !(InventoryUI.selectedItem.GetType() == (typeof(QuestItem)));
		}
		sellButton.Select();
		sellButton.OnSelect(null);
	}

	public void OnSellButton()
	{
		sellWindow.SetActive(true);
		sellWindow.GetComponent<AmtConfirmWindow>().InitialAmt(1);
		gameObject.SetActive(false);
	}
}
