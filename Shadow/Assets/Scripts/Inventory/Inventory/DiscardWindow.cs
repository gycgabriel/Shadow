public class DiscardWindow : AmtConfirmWindow
{
    public override void ConfirmAmt()
    {
        selectedAmt = int.Parse(amtInputField.text);
        if (selectedAmt <= 0)
        {
            PauseMenu.scriptInstance.PopInfoWindow("Amount must be greater than 0.");
        }
        else if (selectedAmt > InventoryUI.selectedItem.GetAmtInInventory())
        {
            PauseMenu.scriptInstance.PopInfoWindow("You do not have that much to discard.");
        }
        else
        {
            amtPanel.SetActive(false);
            confirmPanel.SetActive(true);
            confirmActionText.text = string.Format(
                "Discarding\n" +
                "{0} x {1}\n" +
                "Confirm?",
                InventoryUI.selectedItem.name, selectedAmt);
        }
    }

    // Confirm button on discarding entered amount of items
    public override void ConfirmAction()
    {
        InventoryUI.selectedItem.RemoveFromInventory(selectedAmt);
        PauseMenu.scriptInstance.PopInfoWindow("Discarded!");
        gameObject.SetActive(false);
    }
}
