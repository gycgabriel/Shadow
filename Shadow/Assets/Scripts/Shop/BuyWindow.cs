// The amount confirmation window for buying items from the shop
public class BuyWindow : AmtConfirmWindow
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
            if ((selectedAmt + 1) * InventoryUI.selectedItem.buyPrice <= PartyController.inventory.Gold)
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
            else if (selectedAmt * InventoryUI.selectedItem.buyPrice > PartyController.inventory.Gold)
            {
                ShopMenu.scriptInstance.PopInfoWindow("You do not have enough gold.");
            }
            else
            {
                amtPanel.SetActive(false);
                confirmPanel.SetActive(true);
                confirmActionText.text = string.Format(
                    "Buying\n" +
                    "{0} x {1}\n" +
                    "for\n" +
                    "{2} <sprite=3>\n" +
                    "Confirm?",
                    InventoryUI.selectedItem.name, selectedAmt, InventoryUI.selectedItem.buyPrice * selectedAmt);
            }
        }
        */
        amtPanel.SetActive(false);
        confirmPanel.SetActive(true);
        confirmActionText.text = string.Format(
            "Buying\n" +
            "{0} x {1}\n" +
            "for\n" +
            "{2} <sprite=3>\n" +
            "Confirm?",
            InventoryUI.selectedItem.name, selectedAmt, InventoryUI.selectedItem.buyPrice * selectedAmt);
    }

    // Confirm button on buying entered amount of items
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.BoughtForGold(selectedAmt);
        ShopMenu.scriptInstance.PopInfoWindow("Bought!", GetComponentInParent<InventoryUI>().itemOptionsWindow.selectedSlotBtn);
        gameObject.SetActive(false);
    }
}
