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
        panel.SetActive(true);

        // set texts in quest window
        title.text = quest.title;
        desc.text = quest.desc;
        gold.text = quest.goldReward.ToString();
        exp.text = quest.expReward.ToString();

        accept.onClick.AddListener(delegate () {
            panel.SetActive(false);
            PartyController.questChain = questChain;        // set to null if not questchain
            PartyController.quest = quest;
            quest.Accept();
        });
    }

    public void OpenCompleted(Quest quest, QuestChain questChain = null)
    {
        completedExp.text = quest.expReward.ToString();
        completedGold.text = quest.goldReward.ToString();
        completed.SetActive(true);
        ok.onClick.AddListener(delegate () {
            completed.SetActive(false);
            quest.Complete(delegate() {
                if (questChain != null)
                {
                    Debug.Log("Theres a quest chain");
                    Open(questChain.Next(), questChain);
                } else
                {
                    Debug.Log("No quest chain");
                }
            });
        });
    }

}
