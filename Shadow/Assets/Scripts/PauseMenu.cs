using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : Singleton<PauseMenu>
{
    public static bool gameIsPaused = false;      //Whether the game is paused

    public GameObject pauseMenuUI;               //The UI of the pause menu
    public GameObject statsScreen;
    public GameObject inventoryScreen;
    public GameObject howToPlayUI;

    public string levelToLoad_Menu;              //The name of the scene to be loaded when returning to menu
    public Button buttonToSelect;            // button to select when paused

    private PartyController party;
    private CameraController mainCamera;
    private PlayerStatusWindow playerLevelUI;

    private void Start()
    {
        party = FindObjectOfType<PartyController>();
        mainCamera = FindObjectOfType<CameraController>();
        playerLevelUI = FindObjectOfType<PlayerStatusWindow>(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.X))
        {
            if (statsScreen.activeSelf == true)
            {
                HideStats();
                return;
            }
            if (inventoryScreen.activeSelf == true)
            {
                HideInventory();
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
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        playerLevelUI.gameObject.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        playerLevelUI.gameObject.SetActive(false);
        buttonToSelect.Select();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ReturnToMenu()
    {
        party.Destroy();
        mainCamera.Destroy();
        playerLevelUI.Destroy();
        Time.timeScale = 1f;
        gameIsPaused = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad_Menu);
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

}
