using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AmtConfirmWindow : MonoBehaviour
{
    public int selectedAmt;

    public GameObject amtPanel;
    public GameObject confirmPanel;
    public Button amtConfirmButton;
    public Button amtCancelButton;

    public TMP_Text amtText;
    public Button plusBtn, minusBtn;

    public TMP_Text confirmActionText;

    private void OnEnable()
    {
        amtPanel.SetActive(true);
        confirmPanel.SetActive(false);
        amtCancelButton.Select();
        amtCancelButton.OnSelect(null);
    }

    public abstract void ConfirmAmt();      // Confirm button on entering how much to <action>
    public abstract void ConfirmAction();   // Confirm the action to be done after deciding the amt

    // Cancel button on entering how much to <action>
    public void CancelAmt()
    {
        gameObject.SetActive(false);
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectedSlotBtn.Select();
        GetComponentInParent<InventoryUI>().itemOptionsWindow.selectedSlotBtn.OnSelect(null);
    }

    // Cancel button on <action>ing entered amount of items
    public void CancelAction()
    {
        confirmPanel.SetActive(false);
        amtPanel.SetActive(true);
    }

    public void InitialAmt(int initialAmt)
    {
        selectedAmt = initialAmt;
        amtText.text = "" + selectedAmt;
    }

}