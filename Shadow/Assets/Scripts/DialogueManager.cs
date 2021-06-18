using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    private Queue<string> sentences = new Queue<string>();

    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public bool inDialogue;
    public bool typingDialogue;
    public string currentSentence;

    private System.Action nextDialogue;


    public void StartDialogue(Dialogue dialogue)
    { 
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

    public void StartDialogue(Dialogue dialogue, System.Action nextDialogue)
    {
        StartDialogue(dialogue);
        this.nextDialogue = nextDialogue;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (this.nextDialogue != null)
            {
                System.Action temp = this.nextDialogue;
                this.nextDialogue = null;
                temp();
            } 
            else
            {
                EndDialogue();
            }
            return;

        }

        currentSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    public void ContinueDialogue()
    {
        if (typingDialogue)
        {
            StopCoroutine("TypeSentence");
            dialogueText.text = currentSentence;
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
            yield return null;
        }

        typingDialogue = false;

        // TODO: the arrow thingy to wait for click to move on to next dialogue
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
    }
}
