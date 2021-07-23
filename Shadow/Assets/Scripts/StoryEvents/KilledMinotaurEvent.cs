using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KilledMinotaurEvent : MonoBehaviour
{
    public Quest questSix;
 
    void Update()
    {
        if (StoryManager.scriptInstance.CheckCompletedQuests(questSix) && 
            StoryManager.scriptInstance.CheckEvoked(1, 14) &&
            !DialogueManager.scriptInstance.dialogueBox.activeSelf)
        {
            // Black screen fade
            PartyController.activePC.currentMove = new Vector2(0, -1);      // face down
            SceneManager.LoadScene("Oakheart");
            TransferPlayer.Teleport("rainhouse", new Vector2(0, -1));
        }
    }

    
}
