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
    public float[] direction;

    // Future: Flags of opened loot boxes and clearer dungeon levels
    // stored as dictionary

    /**
     * Constructor to pack data from Player script to PlayerData
     */
    public PlayerData(GameObject playerGO)
    {
        Player player = playerGO.GetComponent<Player>();
        this.stats = player.stats;
        this.skills = player.skills;
        this.statModifiers = player.statModifiers;
        this.currentHP = player.currentHP;
        this.currentMP = player.currentMP;
        this.currentLevel = player.currentLevel;
        this.isDead = player.isDead;
        this.charclass = player.charclass;
        this.currentExp = player.currentExp;
        this.expToNextLevel = player.expToNextLevel;

        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;


        Vector3 playerPosition = playerGO.transform.position;
        position = new float[3];
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        PlayerController playerController = playerGO.GetComponent<PlayerController>();
        direction = new float[2];
        direction[0] = playerController.lastMove.x;
        direction[1] = playerController.lastMove.y;
    }

}
