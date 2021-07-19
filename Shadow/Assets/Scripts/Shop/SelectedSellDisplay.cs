using UnityEngine;
using TMPro;

/**
 * Manage the item description display of item selected by the player
 */
public class SelectedSellDisplay : SelectedItemDisplay
{
    public TMP_Text priceText;
    public GameObject sellWindow;

    public override void UpdateUI()
    {
        // Debug.Log("Updating SelectedItemDisplay.");
        if (InventoryUI.selectedItem == null)
        {
            // Debug.Log("selectedItem: null");
            nameText.text = "";
            descText.text = "";
            priceText.text = " <sprite=3>";
        }
        else
        {
            // Debug.Log("selectedItem: " + InventoryUI.selectedItem.name + " x " + InventoryUI.selectedItem.currentAmt);
            nameText.text = InventoryUI.selectedItem.name;
            descText.text = InventoryUI.selectedItem.description;
            priceText.text = InventoryUI.selectedItem.sellPrice + " <sprite=3>";
        }
    }

    public void OnSellButton()
    {
        sellWindow.SetActive(true);
    }

    private void OnEnable()
    {
        nameText.text = "";
        descText.text = "";
        priceText.text = " <sprite=3>";
    }
}
