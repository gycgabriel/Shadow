using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    private Queue<string> sentences;

    public GameObject dialogueBox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public bool inDialogue;
    public bool typingDialogue;
    public string currentSentence;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
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
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
    }
}
