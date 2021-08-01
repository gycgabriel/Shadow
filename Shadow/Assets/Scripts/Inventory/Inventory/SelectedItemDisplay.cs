using UnityEngine;
using TMPro;

/**
 * Manage the item description display of item selected by the player
 */
public class SelectedItemDisplay : MonoBehaviour
{            
    public TMP_Text nameText;
    public TMP_Text descText;
    public TMP_Text flavorText;

    public virtual void UpdateUI()
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
        UpdateUI();
    }
}
