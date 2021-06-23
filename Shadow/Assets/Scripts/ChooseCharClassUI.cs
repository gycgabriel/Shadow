using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Choose Class UI ignorant of Shadow and Player
 */
public class ChooseCharClassUI : MonoBehaviour
{
    public Button chooseGuardianButton;
    public Button chooseSorcererButton;
    public Button selectClassButton;
    public GameObject confirmWindow;
    public Button confirmClassButton;
    public TMP_Text chosenClassText; 
    public string chosenClass;
    public bool confirmed;

    /**
     * Viewing the Guardian Class details.
     */
    public void SelectingGuardian()
    {
        selectClassButton.gameObject.SetActive(true);
        selectClassButton.interactable = true;
        selectClassButton.onClick.RemoveAllListeners();
        selectClassButton.onClick.AddListener(ConfirmingGuardian);
    }

    /**
     * Viewing the Sorcerer Class details.
     */
    public void SelectingSorcerer()
    {
        selectClassButton.gameObject.SetActive(true);
        selectClassButton.interactable = true;
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
        chosenClass = "Guardian";
        confirmed = true;
        confirmWindow.SetActive(false);
        this.gameObject.SetActive(false);
        chooseGuardianButton.interactable = true;
        chooseSorcererButton.interactable = true;
        selectClassButton.gameObject.SetActive(false);
    }

    public void ChooseSorcerer()
    {
        chosenClass = "Sorcerer";
        confirmed = true;
        confirmWindow.SetActive(false);
        this.gameObject.SetActive(false);
        chooseGuardianButton.interactable = true;
        chooseSorcererButton.interactable = true;
        selectClassButton.gameObject.SetActive(false);
    }

}
