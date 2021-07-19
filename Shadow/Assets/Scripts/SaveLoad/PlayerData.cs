using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    // Creature data of Player
    public Stats playerStats;
    public string[] playerSkills;
    public Dictionary<string, StatModifier> playerStatModifiers;
    public int playerCurrentHP;
    public int playerCurrentMP;
    public int playerCurrentLevel;
    public bool playerIsDead;

    // Creature data of Shadow
    public Stats shadowStats;
    public string[] shadowSkills;
    public Dictionary<string, StatModifier> shadowStatModifiers;
    public int shadowCurrentHP;
    public int shadowCurrentMP;
    public int shadowCurrentLevel;
    public bool shadowIsDead;

    // Player data of Player
    public CharacterClass playerCharclass;
    public CharacterClass shadowCharclass;
    public int currentExp;
    public int expToNextLevel;
    public int playerStatPoints;
    public int shadowStatPoints;

    public bool shadowActive;

    // Location of Player
    public string sceneName;
    public float[] position;
    public float[] direction;

    // Skill cooldowns of Player
    public float[] ultimateSkillCooldown;
    public bool[] isUltimateSkillCooldown;

    public Dictionary<int, Dictionary<int, bool>> evokedStory;

    // Quests
    public SerializableQuestChain questChain;
    public SerializableQuest currQuest;


    // Future: Flags of opened loot boxes and clearer dungeon levels
    // stored as dictionary

    /**
     * Constructor to pack data from Player script to PlayerData
     */
    public PlayerData(GameObject partyGO)
    {
        Player player = partyGO.GetComponentsInChildren<Player>(true)[0];
        this.playerStats = player.stats;
        this.playerSkills = player.skills;
        this.playerStatModifiers = player.statModifiers;
        this.playerCurrentHP = player.currentHP;
        this.playerCurrentMP = player.currentMP;
        this.playerCurrentLevel = player.currentLevel;
        this.playerIsDead = player.isDead;

        Player shadow = partyGO.GetComponentsInChildren<Player>(true)[1];
        this.shadowStats = shadow.stats;
        this.shadowSkills = shadow.skills;
        this.shadowStatModifiers = shadow.statModifiers;
        this.shadowCurrentHP = shadow.currentHP;
        this.shadowCurrentMP = shadow.currentMP;
        this.shadowCurrentLevel = shadow.currentLevel;
        this.shadowIsDead = shadow.isDead;

        this.playerCharclass = player.charclass;
        this.shadowCharclass = shadow.charclass;
        this.shadowActive = PartyController.shadowActive;

        this.currentExp = player.currentExp;
        this.expToNextLevel = player.expToNextLevel;

        this.playerStatPoints = player.statPoints;
        this.shadowStatPoints = shadow.statPoints;

        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;


        Vector3 playerPosition = PartyController.activePC.transform.position;
        position = new float[3];
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        PlayerController playerController = player.GetComponent<PlayerController>();
        direction = new float[2];
        direction[0] = playerController.lastMove.x;
        direction[1] = playerController.lastMove.y;

        ultimateSkillCooldown = PartyController.skillsUIManager.skillCDCounter;
        isUltimateSkillCooldown = PartyController.skillsUIManager.isUltimateSkillCooldown;

        evokedStory = StoryManager.scriptInstance.evokedStory;
        currQuest = PartyController.quest?.SaveQuest();
        questChain = PartyController.questChain?.SaveQuestChain();

        Debug.Log("Saved Story: ");
        foreach (KeyValuePair<int, Dictionary<int, bool>> kvp in evokedStory)
        {
            Debug.Log("Chapter " + kvp.Key + ":");
            foreach (KeyValuePair<int, bool> kvp2 in kvp.Value)
            {
                Debug.Log("Values: " + kvp2.Key + " " + kvp2.Value);
            }
        }
        
    }

}
