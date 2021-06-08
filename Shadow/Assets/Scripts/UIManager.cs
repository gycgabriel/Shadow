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

    public Player thePlayer;                    // The Player

    public Slider hpBar;                            // The Player's HP bar
    public Slider mpBar;                            // The Player's MP bar
    public Slider expBar;                           // The Player's experience bar

    public TMP_Text characlassText;                 // The Text object displaying the Player's character class
    public TMP_Text levelText;                      // The Text object displaying the Player's current level
    public TMP_Text hpText;                         // The Text object displaying the Player's current HP
    public TMP_Text mpText;                         // The Text object displaying the Player's current MP
    public TMP_Text expText;                        // The Text object displaying the Player's current experience points

    void Start()
    {

    }

    void Update()
    {
        Dictionary<string, int> playerStats = thePlayer.getStats();

        // Update the Player's current HP 
        hpBar.maxValue = playerStats["hp"];
        hpBar.value = thePlayer.currentHP;
        hpText.text = thePlayer.currentHP + "/" + playerStats["hp"];

        // Update the Player's current MP
        mpBar.maxValue = playerStats["mp"];
        mpBar.value = thePlayer.currentMP;
        mpText.text = thePlayer.currentMP + "/" + playerStats["mp"];

        // Update the Player's current EXP
        expBar.maxValue = thePlayer.expToNextLevel;
        expBar.value = thePlayer.currentExp;
        expText.text = "" + (thePlayer.expToNextLevel - thePlayer.currentExp);

        // Update the Player's current level
        levelText.text = "" + thePlayer.currentLevel;

        // Update the Player's class (though should be constant)
        characlassText.text = thePlayer.getCharClass();

    }

}
