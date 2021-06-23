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
        // for now
        GameObject player = Instantiate(guardianPrefab, new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        player.transform.parent = Singleton<PartyController>.gameInstance.transform;
        player.GetComponent<Player>().chooseCharClass("Guardian");
        GameObject shadow = Instantiate(sorcererPrefab, new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        shadow.transform.parent = Singleton<PartyController>.gameInstance.transform;
        shadow.GetComponent<Player>().chooseCharClass("Sorcerer");
        shadow.SetActive(false);
        SceneManager.LoadScene("hometown");
    }
}
