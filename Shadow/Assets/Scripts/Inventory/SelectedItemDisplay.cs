using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * Manage the HP Bar display of the current enemy targeted by the player
 */
public class SelectedItemDisplay : MonoBehaviour
{            
    public TMP_Text nameText;
    public TMP_Text descText;
    public TMP_Text flavorText;

    public void UpdateUI()
    {
        // Debug.Log("Updating SelectedItemDisplay.");
        if (InventoryUI.selectedItem == null)
        {
            // Debug.Log("selectedItem: null");
            nameText.text = "";
            descText.text = "";
            flavorText.text = "";
        }
        else
        {
            // Debug.Log("selectedItem: " + InventoryUI.selectedItem.name + " x " + InventoryUI.selectedItem.currentAmt);
            nameText.text = InventoryUI.selectedItem.name;
            descText.text = InventoryUI.selectedItem.description;
            flavorText.text = InventoryUI.selectedItem.flavorText;
        }
    }

    private void OnEnable()
    {
        nameText.text = "";
        descText.text = "";
        flavorText.text = "";
    }
}
