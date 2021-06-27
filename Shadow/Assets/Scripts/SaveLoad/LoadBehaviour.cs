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

    private PartyController party;
    private void Start()
    {
        party = Singleton<PartyController>.scriptInstance;
    }

    public void load(int saveNum)
    {
        PlayerData data = SaveSystem.loadPlayer(saveNum);

        UnityEngine.SceneManagement.SceneManager.LoadScene(data.sceneName);

        if (party == null)
        {
            party = Instantiate(partyPrefab).GetComponent<PartyController>();

            GameObject playerGO = Instantiate(getPrefab(data.playerCharclass.className), new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
            playerGO.transform.parent = Singleton<PartyController>.gameInstance.transform;
            GameObject shadowGO = Instantiate(getPrefab(data.shadowCharclass.className), new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
            shadowGO.transform.parent = Singleton<PartyController>.gameInstance.transform;

            party.initialize(playerGO, shadowGO);

            // Changing Shadow's sprite to a darker color theme
            SpriteRenderer[] shadowSprites = shadowGO.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer shadowSprite in shadowSprites)
            {
                shadowSprite.color = new Color32(0, 100, 170, 255);
            }

            shadowGO.SetActive(false);
        }

        Player player = party.GetComponentsInChildren<Player>(true)[0];
        player.stats = data.playerStats;
        player.skills = data.playerSkills;
        player.statModifiers = data.playerStatModifiers;
        player.currentHP = data.playerCurrentHP;
        player.currentMP = data.playerCurrentMP;
        player.currentLevel = data.playerCurrentLevel;
        player.isDead = data.playerIsDead;

        Player shadow = party.GetComponentsInChildren<Player>(true)[1];
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
        shadow.statPoints = data.shadowStatPoints;

        if (PartyController.shadowActive != data.shadowActive)
        {
            PartyController.switchShadow();
        }

        PartyController.activePC.SetPosition(
            new Vector3(data.position[0], data.position[1], data.position[2]),
            new Vector2(data.direction[0], data.direction[1]));

        PartyController.activePC.skillsUIManager.skillCDCounter = data.ultimateSkillCooldown;
        PartyController.activePC.skillsUIManager.isUltimateSkillCooldown = data.isUltimateSkillCooldown;

        PartyController.activePC.playerMoving = false;
        PartyController.activePC.playerAttacking = false;
        PartyController.inactivePC.playerMoving = false;
        PartyController.inactivePC.playerAttacking = false;
        PartyController.activePC.anim.Play("Base Layer.IdleFace", 0, 0f);
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