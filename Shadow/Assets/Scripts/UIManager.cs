 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script to manage the HP and Exp Bar displays
public class UIManager : MonoBehaviour
{

    public GameObject thePlayer;                    // The Player

    private PlayerStatsManager playerStatsManager;
    private PlayerHealthManager playerHealthManager;
    private PlayerClassingManager playerClassingManager;

    public Slider hpBar;                            // The Player's HP bar
    public Slider mpBar;                            // The Player's MP bar
    public Slider expBar;                           // The Player's experience bar

    public TMP_Text characlassText;                 // The Text object displaying the Player's character class
    public TMP_Text levelText;                      // The Text object displaying the Player's current level
    public TMP_Text hpText;                         // The Text object displaying the Player's current HP
    public TMP_Text mpText;                         // The Text object displaying the Player's current MP
    public TMP_Text expText;                        // The Text object displaying the Player's current experience points

    // Start is called before the first frame update
    void Start()
    {
        playerStatsManager = thePlayer.GetComponent<PlayerStatsManager>();
        playerHealthManager = thePlayer.GetComponent<PlayerHealthManager>();
        playerClassingManager = thePlayer.GetComponent<PlayerClassingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Player's current HP status according to Player Health Manager 
        hpBar.maxValue = playerHealthManager.playerMaxHealth;
        hpBar.value = playerHealthManager.playerCurrentHealth;
        hpText.text = playerHealthManager.playerCurrentHealth + "/" + playerHealthManager.playerMaxHealth;

        // Update the Player's current MP points status according to Player Health Manager 
        mpBar.maxValue = playerHealthManager.playerMaxMana;
        mpBar.value = playerHealthManager.playerCurrentMana;
        mpText.text = playerHealthManager.playerCurrentMana + "/" + playerHealthManager.playerMaxMana;

        // Update the Player's current experience points status according to PlayerStats 
        expBar.maxValue = playerStatsManager.expToNextLevel;
        expBar.value = playerStatsManager.currentExp;
        expText.text = "" + (playerStatsManager.expToNextLevel - playerStatsManager.currentExp);

        // Update the Player's current level according to PlayerStats 
        levelText.text = "" + playerStatsManager.currentLevel;

        // Update the Player's class (though should be constant)
        characlassText.text = playerClassingManager.playerCharaClass.ToString();
    }

}
