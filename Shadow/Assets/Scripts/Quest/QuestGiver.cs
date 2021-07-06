using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

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
        questWindow.panel.SetActive(true);

        // set texts in quest window
        questWindow.title.text = quest.title;
        questWindow.desc.text = quest.desc;
        questWindow.gold.text = quest.goldReward.ToString();
        questWindow.exp.text = quest.expReward.ToString();

        questWindow.accept.onClick.AddListener(delegate() { AcceptQuest(); });
    }

    public void AcceptQuest()
    {
        questWindow.panel.SetActive(false);
        quest.isActive = true;
        PartyController.quest = quest;
    }

}
