using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseClassEvent : MonoBehaviour
{
    // Prefabs of all possible classes
    public GameObject guardianPrefab;
    public GameObject sorcererPrefab;

    public ChooseCharClassUI cccUI;
    public string playerClass;
    public string shadowClass;

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
            GetText.LoadChapter(0);
            GetText.LoadScenario(1);
            Singleton<ScenarioManager>.scriptInstance.PlayScenario(() => {
                cccUI.gameObject.SetActive(true);
            });
        }
        else if (cccUI.confirmed && shadowClass == "")
        {
            shadowClass = cccUI.chosenClass;
            cccUI.confirmed = false;
            cccUI.gameObject.SetActive(false);
            GetText.LoadChapter(0);
            GetText.LoadScenario(2);
            Singleton<ScenarioManager>.scriptInstance.PlayScenario(() => {
                NextScene();
            }); 
        }
    }
    void NextScene() 
    {
        GameObject player = chooseClass(playerClass);
        GameObject shadow = chooseClass(shadowClass);
        shadow.SetActive(false);
        SceneManager.LoadScene("hometown");
    }

    public GameObject chooseClass(string name)        // Guardian, Sorcerer, (first letter caps)
    {
        GameObject player = Instantiate(getPrefab(name), new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        player.transform.parent = Singleton<PartyController>.gameInstance.transform;
        player.GetComponent<Player>().chooseCharClass(name);
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
