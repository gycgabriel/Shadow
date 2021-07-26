using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnBehaviour : MonoBehaviour
{
    public Vector3 loadCoords = new Vector3(21.5f, -4.5f);
    public string loadMap = "Oakheart";

    public int expLossRate = 30;
    public int goldLossRate = 30;

    public void Respawn()
    {
        PartyController.scriptInstance.FullRestore();
        PartyController.scriptInstance.Respawn(expLossRate * 0.01f);
        PartyController.inventory.Gold = Mathf.RoundToInt(PartyController.inventory.Gold * (1f - goldLossRate * 0.01f));

        if (!PartyController.shadowActive)
            PartyController.player.SetActive(true);
        else
            PartyController.shadow.SetActive(true);

        PartyController.activePC.transform.position = loadCoords;

        FadeCanvas.scriptInstance.FadeToScene(loadMap);
    }
}
