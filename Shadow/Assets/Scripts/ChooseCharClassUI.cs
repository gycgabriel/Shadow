using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Choose Class UI ignorant of Shadow and Player
 */
public class ChooseCharClassUI : MonoBehaviour
{
    public Button chooseGuardianButton;
    public Button chooseSorcererButton;
    public ClassDescriptionBox classDescBox;
    public Button selectClassButton;
    public GameObject confirmWindow;
    public Button confirmClassButton;
    public TMP_Text chosenClassText; 
    public string chosenClass;
    public bool confirmed;

    public ClassInfo[] classInfos;

    private void OnEnable()
    {
        StartCoroutine(SelectButtonOnNextFrame(chooseGuardianButton));
    }

    IEnumerator SelectButtonOnNextFrame(Button btn)
    {
        yield return null;
        btn.Select();
        btn.OnSelect(null);
    }

    /**
     * Viewing the selected Class details.
     */
    public void SelectingClass(string className)
    {
        classDescBox.gameObject.SetActive(true);
        classDescBox.DisplayClassInfo(GetClassInfo(className));
        selectClassButton.onClick.RemoveAllListeners();
        selectClassButton.onClick.AddListener(() => ConfirmingClass(className));
    }

    /**
     * Selected the Class, awaiting confirmation.
     */
    public void ConfirmingClass(string className)
    {
        chosenClassText.text = className;
        confirmClassButton.onClick.RemoveAllListeners();
        confirmClassButton.onClick.AddListener(() => ChooseClass(className));
        confirmWindow.SetActive(true);

        confirmClassButton.Select();
        confirmClassButton.OnSelect(null);
    }

    /**
     * Cancel confirmation of class.
     */
    public void StopConfirmingClass()
    {
        confirmWindow.SetActive(false);

        if (chosenClassText.text == "Guardian")
        {
            chooseGuardianButton.Select();
            chooseGuardianButton.OnSelect(null);
        }
        else if (chosenClassText.text == "Sorcerer")
        {
            chooseSorcererButton.Select();
            chooseSorcererButton.OnSelect(null);
        }
    }

    public void ChooseClass(string className)
    {
        chosenClass = className;
        confirmed = true;
        confirmWindow.SetActive(false);
        classDescBox.gameObject.SetActive(false);

        StartCoroutine(SelectButtonOnNextFrame(chooseGuardianButton));
    }

    ClassInfo GetClassInfo(string className)
    {
        return className switch
        {
            "Guardian" => classInfos[0],
            "Sorcerer" => classInfos[1],
            _ => classInfos[0]
        };
    }
}
