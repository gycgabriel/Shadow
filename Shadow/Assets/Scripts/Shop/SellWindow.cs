public class SellWindow : AmtConfirmWindow
{
    public override void ConfirmAmt()
    {
        if (string.IsNullOrEmpty(amtInputField.text))
        {
            ShopMenu.scriptInstance.PopInfoWindow("Please enter something.");
        }
        else
        {
            selectedAmt = int.Parse(amtInputField.text);
            if (selectedAmt <= 0)
            {
                ShopMenu.scriptInstance.PopInfoWindow("Amount must be greater than 0.");
            }
            else if (selectedAmt > InventoryUI.selectedItem.GetAmtInInventory())
            {
                ShopMenu.scriptInstance.PopInfoWindow("You do not have that much to sell.");
            }
            else
            {
                amtPanel.SetActive(false);
                confirmPanel.SetActive(true);
                confirmActionText.text = string.Format(
                    "Selling\n" +
                    "{0} x {1}\n" +
                    "for\n" +
                    "{2} <sprite=3>\n" +
                    "Confirm?",
                    InventoryUI.selectedItem.name, selectedAmt, InventoryUI.selectedItem.sellPrice * selectedAmt);
            }
        }
    }

    // Confirm button on selling entered amount of items
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.SoldForGold(selectedAmt);
        ShopMenu.scriptInstance.PopInfoWindow("Sold!");
        gameObject.SetActive(false);
    }
}

