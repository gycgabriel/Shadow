using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestWindow : Singleton<QuestWindow>
{
    public GameObject panel;
    public GameObject completed;

    public TMP_Text title;
    public TMP_Text desc;
    public TMP_Text gold;
    public TMP_Text exp;
    public Button accept;

    public TMP_Text completedGold;
    public TMP_Text completedExp;
    public Button ok;

    public bool isOpen;

    private void Start()
    {
        panel = transform.GetChild(0).gameObject;
        completed = transform.GetChild(1).gameObject;

        TMP_Text[] textArr = panel.GetComponentsInChildren<TMP_Text>(true);
        title = textArr[1];
        desc = textArr[2];
        gold = textArr[3];
        exp = textArr[4];

        Button[] buttonArr = panel.GetComponentsInChildren<Button>(true);
        accept = buttonArr[0];

        TMP_Text[] ctextArr = completed.GetComponentsInChildren<TMP_Text>(true);
        completedGold = ctextArr[1];
        completedExp = ctextArr[2];

        Button[] cbuttonArr = completed.GetComponentsInChildren<Button>(true);
        ok = cbuttonArr[0];
    }

    public void Open(Quest quest, QuestChain questChain = null)
    {
        //Pause game?
        Time.timeScale = 0f;
        panel.SetActive(true);
        isOpen = true;

        // set texts in quest window
        title.text = quest.title;
        desc.text = quest.desc;
        gold.text = quest.goldReward.ToString();
        exp.text = quest.expReward.ToString();

        accept.onClick.RemoveAllListeners();
        accept.onClick.AddListener(delegate () {
            panel.SetActive(false);
            isOpen = false;
            quest.goal.currentAmt = 0;                      // scriptable objects do not reset in between plays
            PartyController.questChain = questChain;        // set to null if not questchain
            PartyController.quest = quest;
            StoryManager.scriptInstance.SetAcceptedQuest(quest);
            quest.Accept();
            Time.timeScale = 1f;
        });
    }

    public void OpenCompleted(Quest quest, QuestChain questChain = null)
    {
        //Pause game?
        Time.timeScale = 0f;
        completedExp.text = quest.expReward.ToString();
        completedGold.text = quest.goldReward.ToString();
        completed.SetActive(true);
        isOpen = true;

        ok.onClick.RemoveAllListeners();
        ok.onClick.AddListener(delegate () {
            completed.SetActive(false);
            isOpen = false;
            StoryManager.scriptInstance.SetCompletedQuest(quest);
            quest.Complete(delegate() {
                if (questChain != null)
                {
                    Debug.Log("Theres a quest chain");
                    Quest next = questChain.Next();
                    if (next == null)
                    {
                        questChain = null;
                        Debug.Log("Quest chain ends");
                        return;
                    }
                    Open(next, questChain);
                } 
                
                else
                {
                    Debug.Log("No quest chain");
                }
            });
            Time.timeScale = 1f;
        });
    }

}
