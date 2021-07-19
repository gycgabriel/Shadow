using UnityEngine;
using TMPro;

public class ShopMenu : Singleton<ShopMenu>
{
    public static bool gameIsPaused = false;      //Whether the game is paused

    public GameObject ShopMenuUI;
    public GameObject SellUI;               
    public GameObject BuyUI;

    public GameObject infoPanel;

    // Pause game when Player is in the Shop
    public void OpenShop()
    {
        ShopMenuUI.SetActive(true);
        PauseMenu.scriptInstance.Pause();
    }

    // Resume game when Player exits the Shop
    public void CloseShop()
    {
        PauseMenu.scriptInstance.Resume();
        ShopMenuUI.SetActive(false);
    }

    public void ShowBuyUI()
    {
        BuyUI.SetActive(true);
    }

    public void HideBuyUI()
    {
        BuyUI.SetActive(false);
    }

    public void ShowSellUI()
    {
        SellUI.SetActive(true);
    }

    public void HideSellUI()
    {
        SellUI.SetActive(false);
    }

    // Pop up window with a message and an "OK" button
    public void PopInfoWindow(string message)
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponentInChildren<TMP_Text>().text = message;
    }

    // Closing of pop up window
    public void Return()
    {
        infoPanel.SetActive(false);
    }
}