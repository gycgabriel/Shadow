using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;              //The name of the scene to be loaded

    public Button newGameButton, loadButton, howToPlayButton;
    public Button confirmStartButton, confirmNoSaveButton;

    public GameObject confirmNewGameWindow;
    public GameObject confirmNoSaveDataWindow;

    public void PlayGame()
    {
        if (SaveSystem.HaveSaveData(1))
        {
            confirmNewGameWindow.SetActive(true);
            confirmStartButton.Select();
            confirmStartButton.OnSelect(null);
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

    private void OnEnable()
    {
        StartCoroutine(SelectButtonOnNextFrame(newGameButton));
        loadButton.interactable = SaveSystem.HaveSaveData(1);
    }

    IEnumerator SelectButtonOnNextFrame(Button btn)
    {
        yield return null;
        btn.Select();
        btn.OnSelect(null);
    }

    public void CheckLoadGame()
    {
        if (!SaveSystem.HaveSaveData(1))
        {
            confirmNoSaveButton.Select();
            confirmNoSaveButton.OnSelect(null);
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
        FadeCanvas.scriptInstance.FadeToScene(levelToLoad);
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
