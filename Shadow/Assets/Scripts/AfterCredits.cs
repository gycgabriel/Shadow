using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterCredits : MonoBehaviour
{
    void LoadRainHouse()
    {
        FadeCanvas.scriptInstance.FadeWhiteToScene("RainHouse");
        TransferPlayer.Teleport("credits", new Vector2(0, -1));
    }
}
