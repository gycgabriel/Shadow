using UnityEngine;
using TMPro;

/**
 * Manage the item description display of item selected by the player
 */
public class SelectedBuyDisplay : SelectedItemDisplay
{
    public TMP_Text priceText;
    public GameObject buyWindow;

    public override void UpdateUI()
    {
        if (InventoryUI.selectedItem == null)
        {
            nameText.text = "";
            descText.text = "";
            priceText.text = " <sprite=3>";
        }
        else
        {
            nameText.text = InventoryUI.selectedItem.name;
            descText.text = InventoryUI.selectedItem.description;
            priceText.text = InventoryUI.selectedItem.buyPrice + " <sprite=3>";
        }
    }

    public void OnBuyButton()
    {
        if (InventoryUI.selectedItem == null)
        {
            ShopMenu.scriptInstance.PopInfoWindow("Please select something to buy.");
        } 
        else
        {
            buyWindow.SetActive(true);
        }
    }

    private void OnEnable()
    {
        nameText.text = "";
        descText.text = "";
        priceText.text = " <sprite=3>";
    }
}
