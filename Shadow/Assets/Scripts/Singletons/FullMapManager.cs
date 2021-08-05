using System.Collections;
using UnityEngine;

public class FullMapManager : Singleton<FullMapManager>
{
    public static bool MapIsOpen = false;
    public GameObject mapUI;

    private void Update()
    {
        if (!MapIsOpen)
        {
            // Open map if map is not open and nothing else is pausing the game e.g. PauseMenu
            if (Input.GetKeyDown(KeyCode.M) && !PauseMenu.gameIsPaused)
                OpenMap();
        }
        else
        {
            // Close map if map is open
            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
                CloseMap();
        }
    }

    public void OpenMap()
    {
        MapIsOpen = true;
        mapUI.SetActive(true);
        PauseMenu.scriptInstance.Pause();
    }

    public void CloseMap()
    {
        mapUI.SetActive(false);
        // Resume next frame to prevent opening of PauseMenu in the same frame
        StartCoroutine(ResumeNextFrame());
    }
    IEnumerator ResumeNextFrame()
    {
        yield return null;
        MapIsOpen = false;
        PauseMenu.scriptInstance.Resume();
    }
}
