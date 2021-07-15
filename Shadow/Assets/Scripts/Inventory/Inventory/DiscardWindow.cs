using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscardWindow : MonoBehaviour
{
    public int selectedAmt;
    public TMP_InputField discardAmtInputField;
    public TMP_Text confirmDiscardText;

    public GameObject discardAmtPanel;
    public GameObject confirmDiscardPanel;

    private void OnEnable()
    {
        confirmDiscardPanel.SetActive(false);
        discardAmtPanel.SetActive(true);
        discardAmtInputField.text = "";
    }

    // Confirm button on entering how much to discard
    public void ConfirmDiscardAmt()
    {
        selectedAmt = int.Parse(discardAmtInputField.text);
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
            confirmDiscardPanel.SetActive(true);
            discardAmtPanel.SetActive(false);
            confirmDiscardText.text = "Discarding\n" + InventoryUI.selectedItem.name + " x" + selectedAmt + "\nConfirm?";
        }
    }

    // Cancel button on entering how much to discard
    public void CancelDiscardAmt()
    {
        gameObject.SetActive(false);
    }

    // Confirm button on discarding entered amount of items
    public void ConfirmDiscard()
    {
        InventoryUI.selectedItem.RemoveFromInventory(selectedAmt);
        PauseMenu.scriptInstance.PopInfoWindow("Discarded!");
        gameObject.SetActive(false);
    }

    // Cancel button on discarding entered amount of items
    public void CancelDiscard()
    {
        confirmDiscardPanel.SetActive(false);
        discardAmtPanel.SetActive(true);
    }

}
