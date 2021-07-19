 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * Manage the HP and Exp Bar displays
 */
public class UIManager : MonoBehaviour
{
    public bool isStatus;
    public bool isShadow;
    public Player player;

    public Slider hpBar;
    public Slider mpBar;
    public Slider expBar;

    public TMP_Text characlassText;
    public TMP_Text levelText;
    public TMP_Text hpText;
    public TMP_Text mpText;
    public TMP_Text expText;

    void Update()
    {
        if (PartyController.player == null)
            return;

        if (isShadow)
        {
            player = PartyController.shadow.GetComponent<Player>();
        }
        else
        {
            player = PartyController.player.GetComponent<Player>();
        }

        if (isStatus)
        {
            player = PartyController.activePC.gameObject.GetComponent<Player>();
        }

        Dictionary<string, int> playerStats = player.getStats();

        // Update the Player's current HP 
        hpBar.maxValue = playerStats["hp"];
        hpBar.value = player.currentHP;
        hpText.text = player.currentHP + "/" + playerStats["hp"];

        // Update the Player's current MP
        mpBar.maxValue = playerStats["mp"];
        mpBar.value = player.currentMP;
        mpText.text = player.currentMP + "/" + playerStats["mp"];

        // Update the Player's current EXP
        expBar.maxValue = player.expToNextLevel;
        expBar.value = player.currentExp;
        expText.text = "" + (player.expToNextLevel - player.currentExp);

        // Update the Player's current level
        levelText.text = "" + player.currentLevel;

        // Update the Player's class (though should be constant)
        characlassText.text = player.GetCharClass();

    }

}
