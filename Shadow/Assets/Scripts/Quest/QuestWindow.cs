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
    }
}
