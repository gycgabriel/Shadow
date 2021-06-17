using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharClassUI : MonoBehaviour
{
    public GameObject guardianPrefab;
    public GameObject sorcererPrefab;

    public Button chooseGuardianButton;
    public Button chooseSorcererButton;
    public Button selectClassButton;
    public GameObject confirmWindow;
    public Button confirmClassButton;
    public TMP_Text chosenClassText; 
    public string chosenClass;

    public static string playerClass;
    public static string shadowClass;

    /**
     * Viewing the Guardian Class details.
     */
    public void SelectingGuardian()
    {
        selectClassButton.onClick.RemoveAllListeners();
        selectClassButton.onClick.AddListener(ConfirmingGuardian);
    }

    /**
     * Viewing the Sorcerer Class details.
     */
    public void SelectingSorcerer()
    {
        selectClassButton.onClick.RemoveAllListeners();
        selectClassButton.onClick.AddListener(ConfirmingSorcerer);
    }

    /**
     * Selected the Guardian Class, awaiting confirmation.
     */
    public void ConfirmingGuardian()
    {
        chooseGuardianButton.interactable = false;
        chooseSorcererButton.interactable = false;
        selectClassButton.interactable = false;

        chosenClassText.text = "Guardian";
        confirmClassButton.onClick.RemoveAllListeners();
        confirmClassButton.onClick.AddListener(ChooseGuardian);
        confirmWindow.SetActive(true);
    }

    /**
     * Selected the Sorcerer Class, awaiting confirmation.
     */
    public void ConfirmingSorcerer()
    {
        chooseGuardianButton.interactable = false;
        chooseSorcererButton.interactable = false;
        selectClassButton.interactable = false;

        chosenClassText.text = "Sorcerer";
        confirmClassButton.onClick.RemoveAllListeners();
        confirmClassButton.onClick.AddListener(ChooseSorcerer);
        confirmWindow.SetActive(true);
    }

    /**
     * Cancel confirmation of class.
     */
    public void StopConfirmingClass()
    {
        chooseGuardianButton.interactable = true;
        chooseSorcererButton.interactable = true;
        selectClassButton.interactable = true;
        confirmWindow.SetActive(false);
    }

    public void ChooseGuardian()
    {
        GameObject player = Instantiate(guardianPrefab, new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        player.transform.parent = Singleton<PartyController>.gameInstance.transform;
        player.GetComponent<Player>().chooseCharClass("Guardian");
        //for now twice same
        GameObject shadow = Instantiate(sorcererPrefab, new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        shadow.transform.parent = Singleton<PartyController>.gameInstance.transform;
        shadow.GetComponent<Player>().chooseCharClass("Guardian");
        shadow.SetActive(false);
        bringToScene("hometown");
        FindObjectOfType<DialogueManager>().Destroy();

    }

    public void ChooseSorcerer()
    {
        GameObject player = Instantiate(sorcererPrefab, new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        player.GetComponent<Player>().chooseCharClass("Sorcerer");
        bringToScene("hometown");
        FindObjectOfType<DialogueManager>().Destroy();
    }

    public void bringToScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
