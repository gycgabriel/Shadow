using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to manage the Player's stats 
public class PlayerStats : MonoBehaviour
{
    public int currentLevel;                    //The Player's current level
    public int currentExp;                      //The Player's current experience points

    public int[] expToLevelUp;                  //The experience points required to level up at each level

    public int[] HPLevels;                      //The maxHealth stats of the Player at each level
    public int[] attackLevels;                  //The attack stats of the Player at each level
    public int[] defenceLevels;                 //The defence stats of the Player at each level

    public int currentHP;                       //The Player's current maxHealth stat
    public int currentAttack;                   //The Player's current attack stat
    public int currentDefence;                  //The Player's current defence stat

    private PlayerHealthManager playerHealth;   //The Player's PlayerHealthManager component

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the Player's PlayerHealthManager component
        playerHealth = FindObjectOfType<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if current experience points is enough to level up
        if (currentExp >= expToLevelUp[currentLevel])
        {
            //If enough experience points, then the Player levels up
            LevelUp();
        }
    }

    //Function to add experience
    public void AddExperience(int expToAdd)
    {
        currentExp += expToAdd;
    }

    //Function for the Player to level up
    public void LevelUp()
    {
        //Deduct the experience points needed to level up from current experience points
        currentExp -= expToLevelUp[currentLevel];

        //Increment current level
        currentLevel++;

        //Set the Player's new stats according to the Player's new level
        currentHP = HPLevels[currentLevel];
        currentAttack = attackLevels[currentLevel];
        currentDefence = defenceLevels[currentLevel];

        //Update the Player's new maxHealth stat in the PlayerHealthManager
        playerHealth.playerMaxHealth = currentHP;

        //Restore the Player to full HP
        playerHealth.SetMaxHealth();
    }
}
