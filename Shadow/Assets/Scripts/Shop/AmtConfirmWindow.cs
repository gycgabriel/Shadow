using TMPro;
using UnityEngine;

public abstract class AmtConfirmWindow : MonoBehaviour
{
    public int selectedAmt;
    public TMP_InputField amtInputField;

    public GameObject amtPanel;
    public GameObject confirmPanel;

    public TMP_Text confirmActionText;

    private void OnEnable()
    {
        amtPanel.SetActive(true);
        confirmPanel.SetActive(false);
        amtInputField.text = "";
    }

    public abstract void ConfirmAmt();      // Confirm button on entering how much to <action>
    public abstract void ConfirmAction();   // Confirm the action to be done after deciding the amt

    // Cancel button on entering how much to <action>
    public void CancelAmt()
    {
        gameObject.SetActive(false);
    }

    // Cancel button on <action>ing entered amount of items
    public void CancelAction()
    {
        confirmPanel.SetActive(false);
        amtPanel.SetActive(true);
    }

}