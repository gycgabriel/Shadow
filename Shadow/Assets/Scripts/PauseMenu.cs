using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;      //Whether the game is paused

    public GameObject pauseMenuUI;               //The UI of the pause menu
    public string levelToLoad_Menu;              //The name of the scene to be loaded when returning to menu

    private static bool pauseMenuUIExists;

    private PlayerController thePlayer;
    private CameraController mainCamera;
    private UIManager playerLevelUI;

    private void Start()
    {
        if (!pauseMenuUIExists)
        {
            pauseMenuUIExists = true;
            DontDestroyOnLoad(gameObject);
            thePlayer = FindObjectOfType<PlayerController>();
            mainCamera = FindObjectOfType<CameraController>();
            playerLevelUI = FindObjectOfType<UIManager>();
        }
        else
        {
            Destroy(gameObject);
        }

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
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ReturnToMenu()
    {
        thePlayer.DestroyPlayer();
        mainCamera.DestroyCamera();
        playerLevelUI.DestroyLevelUI();
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(levelToLoad_Menu);
        DestroyPauseMenuUI();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }

    public void DestroyPauseMenuUI()
    {
        pauseMenuUIExists = false;
        Destroy(gameObject);
    }


}
