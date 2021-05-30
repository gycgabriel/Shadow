using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for managing enemy's health
public class EnemyHealthManager : MonoBehaviour
{
    public int MaxHealth;                   //The enemy's maximum health points
    public int CurrentHealth;               //The enemy's current health points

    private PlayerStats thePlayerStats;     //The PlayerStats class containing the Player's stats

    public int expToGive;                   //The experience points given by this enemy when defeated

    // Start is called before the first frame update
    void Start()
    {
        //Enemy starts with maximum health
        SetMaxHealth();        

        //Get a component reference to the PlayerStats in the scene
        thePlayerStats = FindObjectOfType<PlayerStats>();      


    }

    // Update is called once per frame
    void Update()
    {
        //If the enemy's health falls to zero
        if (CurrentHealth <= 0)
        {
            //Enemy object is destroyed
            Destroy(gameObject);

            //The Player gains experience points
            thePlayerStats.AddExperience(expToGive);
        }
    }

    //Function for the enemy to take damage
    public void HurtEnemy(int damageToGive)
    {
        //Reduce current health by the damage taken
        CurrentHealth -= damageToGive;
    }

    //Function for restoring health to maximum
    void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
