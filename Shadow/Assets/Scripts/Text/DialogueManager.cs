using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    private Queue<string> sentences = new Queue<string>();

    public float typeSpeed;
    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public bool inDialogue;
    public bool typingDialogue;
    public string currentSentence;

    public bool scenarioOngoing;
    private System.Action onDialogueEnd;

    public void StartDialogue(Dialogue dialogue, System.Action nextAction = null)
    {
        //Pause game?
        Time.timeScale = 0f;
        
        // Assign to keep track for future ContinueText() from button press
        this.onDialogueEnd = nextAction;

        inDialogue = true;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.text)
        {
            sentences.Enqueue(sentence);
        }

        dialogueBox.SetActive(true);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            Debug.Log("Dialogue ended");
            return;
        }

        currentSentence = sentences.Dequeue();

        StopAllCoroutines();

        if (typeSpeed == 0 || typeSpeed > 10)
        {
            dialogueText.text = currentSentence;
        }
        else
        {
            StartCoroutine(TypeSentence(currentSentence));
        }
    }

    public void ContinueDialogue()
    {
        // Show instantly on click again
        if (typingDialogue)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            typingDialogue = false;
        }
        else
        {
            DisplayNextSentence();
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        typingDialogue = true;

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(1 / (typeSpeed * 20));          // 20 is arbituary
        }

        typingDialogue = false;

        // TODO: the arrow thingy to wait for click to move on to next dialogue
    }

    public void EndDialogue()
    {
        //Pause game?
        Time.timeScale = 1f;

        inDialogue = false;

        if (onDialogueEnd != null)
            onDialogueEnd();

        if (scenarioOngoing)
        {
            Singleton<ScenarioManager>.scriptInstance.ContinueText();
        }
        else
        {
            dialogueBox.SetActive(false);
        }
    }
}
