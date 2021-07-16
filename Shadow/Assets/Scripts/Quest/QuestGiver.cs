using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public QuestChain questChain;

    public QuestWindow questWindow;

    private void Update()
    {
        if (questWindow == null)
        {
            questWindow = QuestWindow.scriptInstance;
        }
    }

    public void OpenQuestWindow()
    {
        if (questChain == null)
        {
            questWindow.Open(quest);
        } 
        else
        {
            questWindow.Open(questChain.First(), questChain);
        }
    }

}
