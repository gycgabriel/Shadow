using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateIfNull : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject party;
    public GameObject playerStatus;
    public GameObject enemyStatus;
    public GameObject pauseMenu;

    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
