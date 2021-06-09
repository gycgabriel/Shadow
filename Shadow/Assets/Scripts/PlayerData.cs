using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    // Creature data of Player
    public Stats stats;
    public string[] skills;
    public Dictionary<string, StatModifier> statModifiers;
    public int currentHP;
    public int currentMP;
    public int currentLevel;
    public bool isDead;

    // Player data of Player
    public CharacterClass charclass;
    public int currentExp;
    public int expToNextLevel;

    // Location of Player
    public string sceneName;
    public float[] position;

    // Future: Flags of opened loot boxes and clearer dungeon levels
    // stored as dictionary

    /**
     * Constructor to pack data from Player script to PlayerData
     */
    public PlayerData(Player player)
    {



        sceneName = SceneManager.GetActiveScene().name;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;



    }
}
