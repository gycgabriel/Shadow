using TMPro;

/**
 * Manage the item description display of item selected by the player
 */
public class SelectedShopDisplay : SelectedItemDisplay
{
    public TMP_Text priceText;

    public string shopType;

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
            if (shopType.Equals("Sell"))
                priceText.text = InventoryUI.selectedItem.sellPrice + " <sprite=3>";
            else if (shopType.Equals("Buy"))
                priceText.text = InventoryUI.selectedItem.buyPrice + " <sprite=3>";
            
        }
    }

    private void OnEnable()
    {
        UpdateUI();
    }
}
