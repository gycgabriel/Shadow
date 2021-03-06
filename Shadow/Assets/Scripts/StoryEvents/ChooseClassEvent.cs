using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseClassEvent : MonoBehaviour
{
    // Prefabs of all possible classes
    public GameObject guardianPrefab;
    public GameObject sorcererPrefab;

    public ChooseCharClassUI cccUI;
    public Image guardianSpriteDisplay; // The preview of the Guardian's sprite
    public Image sorcererSpriteDisplay; // The preview of the Sorcerer's sprite

    public string playerClass;
    public string shadowClass;

    // Instantiate where
    public Vector3 loadCoords = new Vector3(-10.5f, 3.5f, 0f);
    public string loadMap = "hometown";

    void Start()
    {
        cccUI = FindObjectOfType<ChooseCharClassUI>();
        cccUI.gameObject.SetActive(true);
    }

    void Update()
    {
        if (cccUI.confirmed && playerClass == "")
        {
            playerClass = cccUI.chosenClass;
            cccUI.confirmed = false;
            cccUI.gameObject.SetActive(false);
            ScenarioManager.scriptInstance.PlayScenario(0, 1, () => {
                cccUI.gameObject.SetActive(true);

                // Change displayed sprites for Shadow to darker color theme
                guardianSpriteDisplay.color = new Color32(0, 100, 170, 255);
                sorcererSpriteDisplay.color = new Color32(0, 100, 170, 255);
            });
        }
        else if (cccUI.confirmed && shadowClass == "")
        {
            shadowClass = cccUI.chosenClass;
            cccUI.confirmed = false;
            cccUI.gameObject.SetActive(false);
            ScenarioManager.scriptInstance.PlayScenario(0, 2, () => {
                NextScene();
            }); 
        }
    }
    void NextScene() 
    {
        GameObject player = chooseClass(playerClass);
        GameObject shadow = chooseClass(shadowClass);

        // Set quests to null in case it was not
        PartyController.quest = null;

        // Changing Shadow's sprite to a darker color theme
        shadow.GetComponentInChildren<PlayerSprite>().isShadow = true;

        shadow.SetActive(false);
        FadeCanvas.scriptInstance.FadeToScene(loadMap);
    }

    public GameObject chooseClass(string name)        // Guardian, Sorcerer, (first letter caps)
    {
        GameObject player = Instantiate(getPrefab(name), loadCoords, Quaternion.identity);
        player.transform.parent = Singleton<PartyController>.gameInstance.transform;
        player.GetComponent<Player>().ChooseCharClass(name);
        return player;
    }


    private GameObject getPrefab(string name)
    {
        switch (name)
        {
            case "Guardian":
                return guardianPrefab;
            case "Sorcerer":
                return sorcererPrefab;
            default:
                Debug.Log("Warning: Class string incorrect, default to Guardian.");
                return guardianPrefab;
        }
    }
}
