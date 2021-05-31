 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script to manage the HP and Exp Bar displays
public class UIManager : MonoBehaviour
{

    public GameObject thePlayer;                    // The Player
    private PlayerStats playerStats;
    private PlayerHealthManager playerHealthManager;

    public Slider hpBar;                            // The Player's HP bar
    public Slider mpBar;                            // The Player's MP bar
    public Slider expBar;                           // The Player's experience bar

    public TMP_Text levelText;                      // The Text object displaying the Player's current level
    public TMP_Text hpText;                         // The Text object displaying the Player's current HP
    public TMP_Text mpText;                         // The Text object displaying the Player's current MP
    public TMP_Text expText;                        // The Text object displaying the Player's current experience points

    // Start is called before the first frame update
    void Start()
    {
        playerStats = thePlayer.GetComponent<PlayerStats>();
        playerHealthManager = thePlayer.GetComponent<PlayerHealthManager>();
        Debug.Log(playerStats);
        Debug.Log(playerHealthManager);
        Debug.Log(playerHealthManager.playerMaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Update the Player's current HP status according to PlayerStats 
        hpBar.maxValue = playerHealthManager.playerMaxHealth;
        hpBar.value = playerHealthManager.playerCurrentHealth;
        hpText.text = playerHealthManager.playerCurrentHealth + "/" + playerHealthManager.playerMaxHealth;

        //Update the Player's current MP points status according to PlayerStats 
        mpBar.maxValue = 260;
        mpBar.value = 250;
        mpText.text = "250/260";

        //Update the Player's current experience points status according to PlayerStats 
        expBar.maxValue = playerStats.expToLevelUp[playerStats.currentLevel];
        expBar.value = playerStats.currentExp;
        expText.text = "" + (playerStats.expToLevelUp[playerStats.currentLevel] - playerStats.currentExp);

        //Update the Player's current level according to PlayerStats 
        levelText.text = "Lvl: " + playerStats.currentLevel;
    }

}
