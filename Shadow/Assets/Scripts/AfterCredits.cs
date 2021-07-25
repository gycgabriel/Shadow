using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterCredits : MonoBehaviour
{
    void LoadRainHouse()
    {
        SceneManager.LoadScene("RainHouse");
        TransferPlayer.Teleport("credits", new Vector2(0, -1));
    }
}
