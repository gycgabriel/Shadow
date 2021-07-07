using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeUI : MonoBehaviour
{
    public GameObject playerGO;
    public GameObject shadowGO;
    public string playerClass;
    public string shadowClass;

    public GameObject mainCamera;
    public GameObject party;
    public GameObject playerStatus;
    public GameObject enemyStatus;
    public GameObject pauseMenu;
    public GameObject dialogueManager;

    void Start()
    {
        PartyController.scriptInstance.Initialize(playerGO, shadowGO);
        playerGO.GetComponent<Player>().ChooseCharClass(playerClass);
        shadowGO.GetComponent<Player>().ChooseCharClass(shadowClass);

        if (Singleton<CameraController>.gameInstance == null)
        {
            Instantiate(mainCamera);
        }

        if (Singleton<PartyController>.gameInstance == null)
        {
            Instantiate(party, transform);
        }

        if (Singleton<PlayerStatusWindow>.gameInstance == null)
        {
            Instantiate(playerStatus);
        }

        if (Singleton<TargetEnemyUIManager>.gameInstance == null)
        {
            Instantiate(enemyStatus);
        }

        if (Singleton<PauseMenu>.gameInstance == null)
        {
            Instantiate(pauseMenu);
        }

        if (Singleton<DialogueManager>.gameInstance == null)
        {
            Instantiate(dialogueManager);
        }
    }

}
