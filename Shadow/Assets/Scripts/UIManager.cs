 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to manage the HP and Exp Bar displays
public class UIManager : MonoBehaviour
{
    public Slider healthBar;                    //The Player's HP bar
    public Text HPText;                         //The Text object displaying the Player's current HP
    public PlayerHealthManager playerHealth;    //The Player's PlayerHealthManager Object

    private PlayerStats playerStats;            //This object's PlayerStats component

    public Slider expBar;                       //The Player's experience bar
    public Text expText;                        //The Text object displaying the Player's current experience points
    public Text levelText;                      //The Text object displaying the Player's current level

    private static bool UIExists;               //Whether the UIManager has been generated

    // Start is called before the first frame update
    void Start()
    {
        //Only one instance of the UIManager will exist at any given time
        //Check if the UIManager exists (has been generated already)
        if (!UIExists)
        {
            //If the UIManager has not been generated yet, generate the UIManager
            //Set UIExists to true so no more UIManagers will be generated
            UIExists = true;

            //The UIManager will persist even when a different scene is loaded
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            //If the UIManager already exists, do not generate another UIManager, destroy this one
            Destroy(gameObject);
        }

        //Get a component reference to this object's PlayerStats
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update the Player's current HP status according to PlayerStats 
        healthBar.maxValue = playerHealth.playerMaxHealth;
        healthBar.value = playerHealth.playerCurrentHealth;
        HPText.text = "HP: " + playerHealth.playerCurrentHealth + "/" + playerHealth.playerMaxHealth;

        //Update the Player's current experience points status according to PlayerStats 
        expBar.maxValue = playerStats.expToLevelUp[playerStats.currentLevel];
        expBar.value = playerStats.currentExp;
        expText.text = "EXP: " + playerStats.currentExp + "/" + playerStats.expToLevelUp[playerStats.currentLevel];

        //Update the Player's current level according to PlayerStats 
        levelText.text = "Lvl " + playerStats.currentLevel;
    }

    public void DestroyLevelUI()
    {
        UIExists = false;
        Destroy(gameObject);
    }
}
