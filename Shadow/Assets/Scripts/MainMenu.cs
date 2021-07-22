using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;              //The name of the scene to be loaded

    public GameObject confirmNewGameWindow;
    public GameObject confirmNoSaveDataWindow;

    public void PlayGame()
    {
        if (SaveSystem.HaveSaveData(1))
        {
            confirmNewGameWindow.SetActive(true);
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                button.interactable = false;
            }
        }
        else
        {
            ConfirmStartNewGame();
        }
    }

    public void CheckLoadGame()
    {
        if (!SaveSystem.HaveSaveData(1))
        {
            confirmNoSaveDataWindow.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting!");
        Application.Quit();
    }

    public void ConfirmStartNewGame()
    {
        SaveSystem.DeleteSaveData(1);
        SceneManager.LoadScene(levelToLoad);
    }

    public void CancelStartNewGame()
    {
        confirmNewGameWindow.SetActive(false);
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
    }

    public void ConfirmNoSaveData()
    {
        confirmNoSaveDataWindow.SetActive(false);
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            button.interactable = true;
        }
    }
}
