using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Button loadButton;

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        PauseTime();
        loadButton.interactable = SaveSystem.HaveSaveData(1);
    }
}
