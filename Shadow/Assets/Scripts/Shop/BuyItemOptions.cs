using UnityEngine;
using UnityEngine.UI;

public class BuyItemOptions : ItemOptions
{
	public Button buyButton;

	public GameObject buyWindow;

	private void OnEnable()
	{
		buyButton.Select();
		buyButton.OnSelect(null);
	}

	public void OnBuyButton()
	{
		if (InventoryUI.selectedItem.buyPrice > PartyController.inventory.Gold)
        {
			ShopMenu.scriptInstance.PopInfoWindow("You do not have enough gold!", selectedSlotBtn);
		}
		else
        {
			buyWindow.SetActive(true);
			buyWindow.GetComponent<AmtConfirmWindow>().InitialAmt(1);
			gameObject.SetActive(false);
		}
	}
}