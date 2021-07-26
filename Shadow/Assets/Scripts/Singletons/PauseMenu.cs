using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : Singleton<PauseMenu>
{
    public static bool gameIsPaused = false;      //Whether the game is paused

    public GameObject pauseMenuUI;               //The UI of the pause menu
    public GameObject statsScreen;
    public GameObject questScreen;
    public GameObject inventoryScreen;
    public GameObject howToPlayUI;

    public string levelToLoad_Menu;              //The name of the scene to be loaded when returning to menu
    public Button buttonToSelect;            // button to select when paused

    public GameObject infoPanel;

    private void Update()
    {
        if (DialogueManager.scriptInstance != null && DialogueManager.scriptInstance.dialogueBox.activeSelf)
            return;

        if (QuestWindow.scriptInstance != null && QuestWindow.scriptInstance.isOpen)
            return;

        if (ShopMenu.scriptInstance.ShopMenuUI.activeSelf == true)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
        {
            if (infoPanel.activeSelf)
            {
                Return();
                return;
            }
            if (statsScreen.activeSelf == true)
            {
                HideStats();
                return;
            }
            if (questScreen.activeSelf == true)
            {
                HideQuests();
                return;
            }
            if (inventoryScreen.activeSelf == true)
            {
                HideInventory();
                return;
            }
            if (howToPlayUI.activeSelf == true)
            {
                howToPlayUI.SetActive(false);
                return;
            }
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && pauseMenuUI.activeSelf)
        {
            GameObject selectedGO = EventSystem.current.currentSelectedGameObject;
            Button selectedButton = selectedGO.GetComponent<Button>();
            if (selectedButton != null)
                selectedButton.onClick.Invoke();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        PlayerStatusWindow.gameInstance.SetActive(true);
        Resume();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        PlayerStatusWindow.gameInstance.SetActive(false);
        buttonToSelect.Select();
        Pause();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(levelToLoad_Menu);
        Destroy(this.gameObject);
        DontDestroyOnLoadManager.DestroyAll();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }


    public void ShowStats()
    {
        statsScreen.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void HideStats()
    {
        statsScreen.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void HideQuests()
    {
        questScreen.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        howToPlayUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void HideHowToPlay()
    {
        howToPlayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void ShowInventory()
    {
        inventoryScreen.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void HideInventory()
    {
        inventoryScreen.SetActive(false);
        pauseMenuUI.SetActive(true);
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

    // Method to pause the game
    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    // Method to resume the game
    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
}
