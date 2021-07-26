using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    private Queue<string> sentences = new Queue<string>();

    public bool autoDialogue;
    public float autoSpeed = 4f;
    public float typeSpeed = 6f;
    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public bool inDialogue;
    public bool typingDialogue;
    public string currentSentence;

    public bool scenarioOngoing;
    private System.Action onDialogueEnd;

    IEnumerator WaitForFade(Dialogue dialogue, System.Action nextAction)
    {
        yield return new WaitUntil(() => FadeCanvas.fadeDone);
        StartDialogue(dialogue, nextAction);
    }


    public void StartDialogue(Dialogue dialogue, System.Action nextAction = null)
    {
        if (!FadeCanvas.fadeDone)
        {
            StartCoroutine(WaitForFade(dialogue, nextAction));
            return;
        }

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
            return;
        }

        currentSentence = sentences.Dequeue();

        StopAllCoroutines();

        if (typeSpeed == 0f || typeSpeed > 2000f)
        {
            dialogueText.text = currentSentence;

            // (instant + auto) = skip text
            if (autoDialogue)
            {
                StartCoroutine(WaitBeforeAutoDialogue());
            }
        }
        else
        {
            StartCoroutine(TypeSentence(currentSentence));
        }
    }

    public void ContinueDialogue()
    {
        // Check quest window if open
        if (QuestWindow.scriptInstance != null && QuestWindow.scriptInstance.isOpen)
            return;

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
            float actualSpeed = 1f / (typeSpeed * 20f);
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(actualSpeed);          // 20 is arbituary
                    // Realtime means cannot pause game mid-typing
        }

        typingDialogue = false;

        if (autoDialogue)
        {
            StartCoroutine(WaitBeforeAutoDialogue());
        }

        // TODO: the arrow thingy to wait for click to move on to next dialogue
    }

    IEnumerator WaitBeforeAutoDialogue()
    {
        yield return new WaitForSecondsRealtime(10f / (autoSpeed * 4f));
        ContinueDialogue();
    }

    public void EndDialogue()
    {
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
            Time.timeScale = 1f;
        }
    }

    public void SkipDialogue(bool skipInput)
    {
        if (skipInput)
        {
            typeSpeed = 200f;
            autoSpeed = 10f;
            autoDialogue = true;
            // for instant text when skipping
            if (typingDialogue)
                Singleton<ScenarioManager>.scriptInstance.ContinueText();
            StartCoroutine("WaitBeforeAutoDialogue");
        } 
        else
        {
            typeSpeed = 6f;
            autoSpeed = 4f;
            autoDialogue = false;
            StopCoroutine("WaitBeforeAutoDialogue");
        }
    }
}
