using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUIManager : Singleton<SkillsUIManager>
{
    const int PlayerChar = 0, ShadowChar = 1;

    public Image activeUltimateSkillImage;
    public Image activeUltimateSkillCDImage;

    public Image inactiveUltimateSkillImage;
    public Image inactiveUltimateSkillCDImage;

    public SkillSet[] skillSet;
    public float[] skillCDCounter;
    public bool[] isUltimateSkillCooldown;

    // Start is called before the first frame update
    void Start()
    {
        if (skillSet.Length == 0)
        {
            skillSet = new SkillSet[2];
            Debug.Log("initializing skill");
        }
        if (skillCDCounter.Length == 0)
        {
            skillCDCounter = new float[2] { 0, 0 };
        }
        if (isUltimateSkillCooldown.Length == 0)
        {
            isUltimateSkillCooldown = new bool[2] { false, false };
        }
        Debug.Log("skill.length: " + skillSet.Length);
    }

    // Update is called once per frame
    void Update()
    {
        skillSet[PlayerChar] = PartyController.playerPC.playerSkillSet;
        skillSet[ShadowChar] = PartyController.shadowPC.playerSkillSet;

        // Update skill cooldown timers
        UpdateSkillCooldown(PlayerChar);
        UpdateSkillCooldown(ShadowChar);

        // Update skill cooldown display based on active character
        UpdateSkillCDImage(PartyController.shadowActive);
    }

    public void UseUltimateSkill(bool shadowActive)
    {
        // if shadowActive is true, then active character is Shadow, else is Player
        int character = shadowActive ? ShadowChar : PlayerChar;

        // Start skill cooldown timer
        isUltimateSkillCooldown[character] = true;
        skillCDCounter[character] = skillSet[character].ultimateSkill.skillCooldown;

        // Update skill cooldown image
        UpdateSkillCDImage(shadowActive);
    }

    private void UpdateSkillCooldown(int character)
    {
        if (isUltimateSkillCooldown[character])
        {
            skillCDCounter[character] -= Time.deltaTime;

            if (skillCDCounter[character] <= 0)
            {
                skillCDCounter[character] = 0;
                isUltimateSkillCooldown[character] = false;
            }
        }
    }

    private void UpdateSkillCDImage(bool shadowActive)
    {
        // if shadowActive is true, then active character is Shadow, else is Player
        int activeChar = shadowActive ? ShadowChar : PlayerChar;
        int inactiveChar = shadowActive ? PlayerChar : ShadowChar;

        activeUltimateSkillImage.sprite = skillSet[activeChar].ultimateSkill.skillIcon;
        activeUltimateSkillCDImage.sprite = skillSet[activeChar].ultimateSkill.skillIcon;
        activeUltimateSkillCDImage.fillAmount = skillCDCounter[activeChar] / skillSet[activeChar].ultimateSkill.skillCooldown;

        inactiveUltimateSkillImage.sprite = skillSet[inactiveChar].ultimateSkill.skillIcon;
        inactiveUltimateSkillCDImage.sprite = skillSet[inactiveChar].ultimateSkill.skillIcon;
        inactiveUltimateSkillCDImage.fillAmount = skillCDCounter[inactiveChar] / skillSet[inactiveChar].ultimateSkill.skillCooldown;
    }

    /**
     * Checks ultimate skill cooldown. shadowActive determines to check player's or shadow's cooldown.
     */
    public bool IsUltimateSkillOnCooldown(bool shadowActive)
    {
        int character = shadowActive ? ShadowChar : PlayerChar;

        return isUltimateSkillCooldown[character];
    }
}
