// The amount confirmation window for selling items to the shop
public class SellWindow : AmtConfirmWindow
{
    private void Start()
    {
        minusBtn.onClick.AddListener(() =>
        {
            if (selectedAmt > 1)
                selectedAmt--;
            amtText.text = "" + selectedAmt;
        });

        plusBtn.onClick.AddListener(() =>
        {
            if (selectedAmt < InventoryUI.selectedItem.GetAmtInInventory())
                selectedAmt++;
            amtText.text = "" + selectedAmt;
        });
    }

    public override void ConfirmAmt()
    {
        /*
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
        */

        amtPanel.SetActive(false);
        confirmPanel.SetActive(true);
        confirmActionText.text = string.Format(
            "Selling\n" +
            "{0} x {1}\n" +
            "for\n" +
            "{2} <sprite=3>\n" +
            "Confirm?",
            InventoryUI.selectedItem.name, base.selectedAmt, InventoryUI.selectedItem.sellPrice * base.selectedAmt);
    }

    // Confirm button on selling entered amount of items
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.SoldForGold(base.selectedAmt);
        ShopMenu.scriptInstance.PopInfoWindow("Sold!", GetComponentInParent<InventoryUI>().itemOptionsWindow.selectedSlotBtn);
        gameObject.SetActive(false);
    }
}

