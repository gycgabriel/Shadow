using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBehaviour : MonoBehaviour
{
    // Prefabs of all possible classes
    public GameObject guardianPrefab;
    public GameObject sorcererPrefab;
    public GameObject partyPrefab;
    public GameObject scenarioStoryManagerPrefab;
    public ItemDex itemDexPrefab;

    public void load(int saveNum)
    {
        PlayerData data = SaveSystem.loadPlayer(saveNum);

        if (data == null)
            return;

        SceneManager.LoadScene("Loading");

        if (PartyController.scriptInstance == null)
            Instantiate(partyPrefab);

        if (StoryManager.scriptInstance == null)
            Instantiate(scenarioStoryManagerPrefab);

        GameObject partyGO = PartyController.gameInstance;

        Debug.Log(partyGO);

        if (partyGO.transform.childCount == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                Destroy(partyGO.transform.GetChild(i).gameObject);
            }
            partyGO.transform.DetachChildren();
        }

        if (partyGO.transform.childCount == 0)
        {
            GameObject playerGO = Instantiate(getPrefab(data.playerCharclass.className));
            playerGO.transform.parent = partyGO.transform;
            GameObject shadowGO = Instantiate(getPrefab(data.shadowCharclass.className));
            shadowGO.transform.parent = partyGO.transform;

            PartyController.scriptInstance.Initialize(playerGO, shadowGO);

            SpriteRenderer[] shadowSprites = PartyController.shadow.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer shadowSprite in shadowSprites)
            {
                shadowSprite.color = new Color32(0, 100, 170, 255);
            }

            shadowGO.SetActive(false);
        }

        Player player = partyGO.GetComponentsInChildren<Player>(true)[0];
        player.stats = data.playerStats;
        player.skills = data.playerSkills;
        player.statModifiers = data.playerStatModifiers;
        player.currentHP = data.playerCurrentHP;
        player.currentMP = data.playerCurrentMP;
        player.currentLevel = data.playerCurrentLevel;
        player.isDead = data.playerIsDead;

        Player shadow = partyGO.GetComponentsInChildren<Player>(true)[1];
        shadow.stats = data.shadowStats;
        shadow.skills = data.shadowSkills;
        shadow.statModifiers = data.shadowStatModifiers;
        shadow.currentHP = data.shadowCurrentHP;
        shadow.currentMP = data.shadowCurrentMP;
        shadow.currentLevel = data.shadowCurrentLevel;
        shadow.isDead = data.shadowIsDead;

        player.charclass = data.playerCharclass;
        shadow.charclass = data.shadowCharclass;
        player.currentExp = data.currentExp;
        player.expToNextLevel = data.expToNextLevel;
        player.statPoints = data.playerStatPoints;
        shadow.currentExp = data.currentExp;
        shadow.expToNextLevel = data.expToNextLevel;
        shadow.statPoints = data.shadowStatPoints;

        if (PartyController.shadowActive != data.shadowActive)
            PartyController.SwitchShadow();

        PartyController.activePC.SetPosition(
            new Vector3(data.position[0], data.position[1], data.position[2]),
            new Vector2(data.direction[0], data.direction[1]));

        PartyController.skillsUIManager.skillCDCounter = data.ultimateSkillCooldown;
        PartyController.skillsUIManager.isUltimateSkillCooldown = data.isUltimateSkillCooldown;

        PartyController.activePC.playerMoving = false;
        PartyController.activePC.playerAttacking = false;
        PartyController.inactivePC.playerMoving = false;
        PartyController.inactivePC.playerAttacking = false;
        PartyController.activePC.anim.Play("Base Layer.IdleFace", 0, 0f);

        StoryManager.scriptInstance.evokedStory = data.evokedStory;
        StoryManager.scriptInstance.acceptedQuests = data.acceptedQuests;
        StoryManager.scriptInstance.completedQuests = data.completedQuests;

        PartyController.quest = Quest.LoadQuest(data.currQuest);
        PartyController.questChain = QuestChain.LoadQuestChain(data.questChain);

        ItemDex itemDex = Instantiate(itemDexPrefab);
        PartyController.inventory.ClearInventory();
        for (int i = 0; i < data.inventory.GetLength(0); i++)
        {
            Item item = Instantiate(itemDex.GetItem(data.inventory[i, 0]));
            item.currentAmt = data.inventory[i, 1];
            PartyController.inventory.Add(item);
        }
        PartyController.inventory.Gold = data.gold;

        PartyController.itemHotkeyUIManager.LoadHotkeys(data.hotkeys);
        PartyController.itemHotkeyUIManager.LoadHotkeyCooldown(data.hotkeyCooldown);
        PartyController.itemHotkeyUIManager.LoadIsHotkeyOnCooldown(data.isHotkeyOnCooldown);

        // Finally bring to scene
        SceneManager.LoadScene(data.sceneName);
    }

    private GameObject getPrefab(string name)
    {
        switch (name)
        {
            case "Guardian":
                return guardianPrefab;
            case "Sorcerer":
                return sorcererPrefab;
            default:
                Debug.Log("Warning: Class string incorrect, default to Guardian.");
                return guardianPrefab;
        }
    }
}
