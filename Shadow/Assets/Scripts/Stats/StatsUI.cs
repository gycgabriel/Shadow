using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StatsUI : MonoBehaviour
{
    public Button defaultSelectedBtn;

    public bool isShadow;

    public Player player;

    public Image portrait;

    public TMP_Text nameText;
    public TMP_Text characlassText;
    public TMP_Text levelText;
    public TMP_Text pointsText;
    public TMP_Text[] statTexts;
    public Button[] statPointsButtons;
    public int[] allocatedPoints;
    private int tempPoints;

    private bool isMagic;

    private void Start()
    {
        statTexts = GetComponentsInChildren<TMP_Text>();
        statPointsButtons = GetComponentsInChildren<Button>(true);      // true to get inactive too
        allocatedPoints = new int[8];

        for (int i = 0; i < statPointsButtons.Length; i++)
        {
            // Plus button
            if (i % 2 == 0)
            {
                int x = i / 2;              // each button has own x
                statPointsButtons[i].onClick.AddListener(() =>
                {
                    if (tempPoints > 0)
                    {
                        allocatedPoints[x]++;
                        tempPoints--;
                    }
                });
            }
            // Minus button
            else
            {
                int x = i / 2;              // each button has own x
                statPointsButtons[i].onClick.AddListener(() =>
                {
                    if (allocatedPoints[x] > 0)
                    {
                        allocatedPoints[x]--;
                        tempPoints++;
                    }
                });
            }
        }
    }

    private void OnEnable()
    {
        if (isShadow)
        {
            nameText.text = "Pancakes";
            player = PartyController.shadow.GetComponent<Player>();
            portrait.color = new Color32(0, 100, 170, 255);
        }
        else
        {
            nameText.text = "Rain";
            player = PartyController.player.GetComponent<Player>();
            portrait.color = Color.white;
        }
        ClearPoints();
    }


    void Update()
    {
        if (tempPoints > player.statPoints)
        {
            tempPoints = player.statPoints;
        }

        nameText.text = !isShadow ? "Rain" : "Pancakes";
        portrait.sprite = player.gameObject.GetComponent<PortraitBehaviour>().portraitToDisplay;
        characlassText.text = player.GetCharClass();

        if (player.GetCharClass() == "Sorcerer")
            isMagic = true;
        else
            isMagic = false;

        levelText.text = "" + player.currentLevel;
        pointsText.text = "" + tempPoints;
        
        statTexts[0].text = "HP";
        statTexts[1].text = "" + (player.getBaseStats()["hp"] + allocatedPoints[0]);
        statTexts[2].text = "MP";
        statTexts[3].text = "" + (player.getBaseStats()["mp"] + allocatedPoints[1]);

        // Show either atk or matk
        if (isMagic)
        {
            statTexts[4].text = "Matk";
            statTexts[5].text = "" + (player.getBaseStats()["matk"] + allocatedPoints[2]);
        } else
        {
            statTexts[4].text = "Atk";
            statTexts[5].text = "" + (player.getBaseStats()["atk"] + allocatedPoints[2]);
        }

        statTexts[6].text = "Def";
        statTexts[7].text = "" + (player.getBaseStats()["def"] + allocatedPoints[3]);
        statTexts[8].text = "Mdef";
        statTexts[9].text = "" + (player.getBaseStats()["mdef"] + allocatedPoints[4]);
        /*statTexts[12].text = "Agi";
        statTexts[13].text = "" + (player.getBaseStats()["agi"] + allocatedPoints[6]);
        statTexts[14].text = "Luk";
        statTexts[15].text = "" + (player.getBaseStats()["luk"] + allocatedPoints[7]);*/

        
        for (int i = 0; i < statPointsButtons.Length; i++)
        {
            // Plus button
            if (i % 2 == 0)
            {
                if (tempPoints > 0)
                {
                    statPointsButtons[i].interactable = true;
                }
                else
                {
                    if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == statPointsButtons[i])
                    {
                        // select minus on same row
                        if (i + 1 <= statPointsButtons.Length-1 && statPointsButtons[i + 1].interactable)
                            statPointsButtons[i + 1].Select();
                        else
                            defaultSelectedBtn.Select();
                    }
                    statPointsButtons[i].interactable = false;
                }
            }

            // Minus button
            else
            {
                if (allocatedPoints[i/2] > 0)
                {
                    statPointsButtons[i].interactable = true;
                }
                else
                {
                    if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == statPointsButtons[i])
                    {
                        // select plus on same row
                        if (i - 1 >= 0 && statPointsButtons[i - 1].interactable)
                            statPointsButtons[i - 1].Select();
                        else
                            defaultSelectedBtn.Select();
                    }
                        
                    statPointsButtons[i].interactable = false;
                }
            }
        }

        // UpdateStatButtonsIsActive();
    }

    public void ShowShadowStats()
    {
        isShadow = true;
        OnEnable();
    }

    public void ShowPlayerStats()
    {
        isShadow = false;
        OnEnable();
    }

    public void ConfirmPoints()
    {
        for (int i = 0; i < allocatedPoints.Length; i++)
        {
            if (allocatedPoints[i] > 0)
            {
                string statName = i switch
                {
                    0 => "hp",
                    1 => "mp",
                    2 => "atk",
                    3 => "def",
                    4 => "mdef",
                    5 => "agi",
                    6 => "luk",
                    7 => "luk",
                    _ => "hp"
                };

                // catch atk and assign matk if magic
                if (statName == "atk" && isMagic)
                    statName = "matk";

                if (player.statPoints - allocatedPoints[i] >= 0)
                {
                    player.stats.addBaseStat(statName, allocatedPoints[i]);
                    player.statPoints -= allocatedPoints[i];
                }
            }
        }

        ClearPoints();      // reset points shown

    }

    public void ClearPoints()
    {
        allocatedPoints = new int[8];
        tempPoints = player.statPoints;
        UpdateStatButtonsIsActive();
    }

    void UpdateStatButtonsIsActive()
    {
        bool isActive = (player.statPoints != 0);
        foreach (Button btn in statPointsButtons)
        {
            btn.gameObject.SetActive(isActive);
        }
    }

}
