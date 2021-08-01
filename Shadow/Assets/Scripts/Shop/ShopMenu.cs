using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ShopMenu : Singleton<ShopMenu>
{
    public static bool gameIsPaused = false;      //Whether the game is paused

    public GameObject ShopMenuUI;               
    public GameObject BuyUI;
    public GameObject SellUI;

    public GameObject infoPanel;

    public Button buyBtn, sellBtn;

    // Pause game when Player is in the Shop
    public void OpenShop()
    {
        ShopMenuUI.SetActive(true);
        PauseMenu.scriptInstance.Pause();
        SelectButton(buyBtn);
    }

    // Resume game when Player exits the Shop
    public void CloseShop()
    {
        ShopMenuUI.SetActive(false);
        // Resume next frame to prevent triggering interaction with Shop in the same frame again
        StartCoroutine(ResumeNextFrame());
    }

    IEnumerator ResumeNextFrame()
    {
        yield return null;
        PauseMenu.scriptInstance.Resume();
    }

    public void ShowBuyUI()
    {
        BuyUI.SetActive(true);
    }

    public void HideBuyUI()
    {
        BuyUI.SetActive(false);
        SelectButton(buyBtn);
    }

    public void ShowSellUI()
    {
        SellUI.SetActive(true);
    }

    public void HideSellUI()
    {
        SellUI.SetActive(false);
        SelectButton(sellBtn);
    }

    // Pop up window with a message and an "OK" button
    public void PopInfoWindow(string message)
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<TMP_Text>().text = message;
        SelectButton(infoPanel.GetComponentInChildren<Button>());
    }

    // Overload pop up window with which button to select after closing window
    public void PopInfoWindow(string message, Button btn)
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<TMP_Text>().text = message;
        Button infoBtn = infoPanel.GetComponentInChildren<Button>();
        infoBtn.onClick.AddListener(() =>
        {
            btn.Select();
            btn.OnSelect(null);
        });
        SelectButton(infoBtn);
    }

    // Closing of pop up window
    public void Return()
    {
        infoPanel.SetActive(false);
    }

    public void SelectButton(Button btn)
    {
        if (btn != null)
        {
            btn.Select();
            btn.OnSelect(null);
        }
    }
}