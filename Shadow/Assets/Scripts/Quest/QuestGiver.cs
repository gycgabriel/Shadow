using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public QuestChain questChain;

    public void OpenQuestWindow()
    {
        if (QuestWindow.scriptInstance == null)
            return;

        if (questChain == null)
        {
            QuestWindow.scriptInstance.Open(quest);
        } 
        else
        {
            QuestWindow.scriptInstance.Open(questChain.First(), questChain);
        }
    }

}
