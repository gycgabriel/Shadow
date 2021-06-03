using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to manage the Player's stats 
public class PlayerStatsManager : MonoBehaviour
{
    public int currentLevel { get; private set; }
    public int currentExp { get; private set; }
    public int expToNextLevel { get; private set; }                 // Full exp of this level not accounting for exp already gained;

    public enum Stats { HP, MP, ATK, DEF, MATK, MDEF, AGI, LUK };
    public int[] playerStats = new int[] { 100, 50, 20, 20, 20, 20, 20, 0 };

    // Math formulas
    private System.Func<int, int> expFormula = x => Mathf.FloorToInt(Mathf.Pow(x, 3f) + 14f * x);           // Exp formula n^3 + 14n for now
    private System.Func<int, int> hpFormula = x => x + 50;                                                  // Hp formula n + 50 for now
    // TODO: all formulae for every stat

    private PlayerHealthManager playerHealth;                       // The Player's PlayerHealthManager component

    // Start is called before the first frame update
    void Start()
    {
        // Initialise level and exp
        currentLevel = 1;
        currentExp = 0;
        expToNextLevel = expFormula(currentLevel);

        // PlayerHealthManager needed to update curr max health
        playerHealth = this.gameObject.GetComponent<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if current experience points is enough to level up
        if (currentExp >= expToNextLevel)
        {
            // If enough experience points, then the Player levels up
            LevelUp();
        }
    }

    // Function to add experience
    public void AddExperience(int expToAdd)
    {
        currentExp += expToAdd;
    }

    // Function for the Player to level up
    public void LevelUp()
    {
        // Deduct the experience points needed to level up from current experience points
        currentExp -= expToNextLevel;

        // Increment current level
        currentLevel++;

        // Set requirement for next level
        expToNextLevel = expFormula(currentLevel);

        // Set the Player's new stats according to the Player's new level
        playerStats[(int) Stats.HP] += hpFormula(currentLevel);
        playerStats[(int) Stats.MP] += 20;

        // Update the Player's new maxHealth stat in the PlayerHealthManager
        playerHealth.playerMaxHealth = playerStats[(int) Stats.HP];
        playerHealth.playerMaxMana = playerStats[(int) Stats.MP];

        // TODO: Raise stats on level up based on formulae above 

        // Restore the Player to full HP
        playerHealth.ResetMaxHealth();
        playerHealth.ResetMaxMana();
    }
}
