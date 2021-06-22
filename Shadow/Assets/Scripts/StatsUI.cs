using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public bool isShadow;

    public Player player;

    public TMP_Text nameText;
    public TMP_Text characlassText;
    public TMP_Text levelText;
    public TMP_Text[] statTexts;

    private void Start()
    {
        statTexts = GetComponentsInChildren<TMP_Text>();
    }

    void Update()
    {
        if (isShadow)
        {
            player = PartyController.shadow.GetComponent<Player>();
        }
        else
        {
            player = PartyController.player.GetComponent<Player>();
        }

        characlassText.text = player.getCharClass();
        levelText.text = "" + player.currentLevel;
        statTexts[0].text = "HP";
        statTexts[1].text = "" + player.getBaseStats()["hp"];
        statTexts[2].text = "MP";
        statTexts[3].text = "" + player.getBaseStats()["mp"];
        statTexts[4].text = "Atk";
        statTexts[5].text = "" + player.getBaseStats()["atk"];
        statTexts[6].text = "Def";
        statTexts[7].text = "" + player.getBaseStats()["def"];
        statTexts[8].text = "Matk";
        statTexts[9].text = "" + player.getBaseStats()["matk"];
        statTexts[10].text = "Mdef";
        statTexts[11].text = "" + player.getBaseStats()["mdef"];
        statTexts[12].text = "Agi";
        statTexts[13].text = "" + player.getBaseStats()["agi"];
        statTexts[14].text = "Luk";
        statTexts[15].text = "" + player.getBaseStats()["luk"];
    }

    public void ShowShadowStats()
    {
        isShadow = true;
    }

    public void ShowPlayerStats()
    {
        isShadow = false;
    }

}
