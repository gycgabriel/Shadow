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

    public GameObject infoPanel;

    public Transform LHSBtnParent;

    /* LHS Buttons:
     * 0 - Resume
     * 1 - Stats
     * 2 - Quests
     * 3 - Inventory
     * 4 - Save
     * 5 - Load
     * 6 - How To Play
     * 7 - Settings
     * 8 - Return to Title
     * 9 - Quit
     */
    Button[] LHSBtns;

    private void OnEnable()
    {
        LHSBtns = LHSBtnParent.GetComponentsInChildren<Button>();
    }

    private void Update()
    {
        if (!FadeCanvas.fadeDone)
            return;

        if (DialogueManager.scriptInstance != null && DialogueManager.scriptInstance.dialogueBox.activeSelf)
            return;

        if (QuestWindow.scriptInstance != null && QuestWindow.scriptInstance.isOpen)
            return;

        if (ShopMenu.scriptInstance != null && ShopMenu.scriptInstance.ShopMenuUI.activeSelf == true)
            return;

        if (FullMapManager.scriptInstance != null && FullMapManager.MapIsOpen)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
        {
            if (infoPanel.activeSelf)
            {
                Return();
                return;
            }
            if (statsScreen.activeSelf)
            {
                HideStats();
                return;
            }
            if (questScreen.activeSelf)
            {
                HideQuests();
                return;
            }
            if (inventoryScreen.activeSelf)
            {
                HideInventory();
                return;
            }
            if (howToPlayUI.activeSelf)
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

        /* Deprecated, have set 'Z' as alt submit input in project's input manager settings
        if (gameIsPaused && pauseMenuUI.activeSelf && Input.GetKeyDown(KeyCode.Z) &&
            !infoPanel.activeSelf &&
            !statsScreen.activeSelf &&
            !questScreen.activeSelf &&
            !inventoryScreen.activeSelf &&
            !howToPlayUI.activeSelf)
        {
            GameObject selectedGO = EventSystem.current.currentSelectedGameObject;
            if (selectedGO != null)
            {
                Button selectedButton = selectedGO.GetComponent<Button>();
                if (selectedButton != null)
                    selectedButton.onClick.Invoke();
            }
        }
        */
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
        SelectButton(LHSBtns[0]);
        Pause();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        FadeCanvas.scriptInstance.FadeOut();
        SceneManager.LoadScene(levelToLoad_Menu);

        // Set quests to null in case it was not
        PartyController.quest = null;

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
        SelectButton(LHSBtns[1]);
    }

    public void HideQuests()
    {
        questScreen.SetActive(false);
        pauseMenuUI.SetActive(true);
        SelectButton(LHSBtns[2]);
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
        SelectButton(LHSBtns[3]);
    }

    public void ShowHowToPlay()
    {
        howToPlayUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        SelectButton(howToPlayUI.GetComponentInChildren<Button>());
    }

    public void HideHowToPlay()
    {
        howToPlayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        SelectButton(LHSBtns[6]);
    }
    
    public void PopSavedWindow()
    {
        PopInfoWindow("Saved!", LHSBtns[4]);
    }

    public void PopLoadedWindow()
    {
        StartCoroutine(PopInfoWindowInNextFrame("Loaded!", LHSBtns[5]));
    }

    IEnumerator PopInfoWindowInNextFrame(string message, Button btn)
    {
        yield return null;
        PopInfoWindow(message, btn);
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

    // Method to pause the game
    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    // Method to resume the game
    public void Resume()
    {
        StartCoroutine(ResumeNextFrame());
    }

    // Resume game in the next frame, to prevent sensing input in same frame as when game resumes
    IEnumerator ResumeNextFrame()
    {
        yield return null;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void SelectButton(Button btn)
    {
        if (btn != null)
        {
            StartCoroutine(SelectButtonOnNextFrame(btn));
        }
    }

    IEnumerator SelectButtonOnNextFrame(Button btn)
    {
        yield return null;
        btn.Select();
        btn.OnSelect(null);
    }
}
