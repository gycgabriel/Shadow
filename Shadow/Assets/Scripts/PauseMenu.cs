using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : Singleton<PauseMenu>
{
    public static bool gameIsPaused = false;      //Whether the game is paused

    public GameObject pauseMenuUI;               //The UI of the pause menu
    public string levelToLoad_Menu;              //The name of the scene to be loaded when returning to menu

    private PlayerController thePlayer;
    private CameraController mainCamera;
    private StatusDrawer playerLevelUI;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        mainCamera = FindObjectOfType<CameraController>();
        playerLevelUI = FindObjectOfType<StatusDrawer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ReturnToMenu()
    {
        thePlayer.DestroyPlayer();
        mainCamera.DestroyCamera();
        playerLevelUI.Destroy();
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(levelToLoad_Menu);
        Destroy(this.gameObject);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }

}
