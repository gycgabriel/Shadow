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

    // Player's skill 
    public Skill[] skill;
    public float[] skillCDCounter;
    public bool[] isUltimateSkillCooldown;

    // Start is called before the first frame update
    void Start()
    {
        if (skill.Length == 0)
        {
            skill = new Skill[2];
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
        Debug.Log("skill.length: " + skill.Length);
    }

    // Update is called once per frame
    void Update()
    {

        skill[PlayerChar] = PartyController.playerPC.playerSkills.ultimateSkill;
        skill[ShadowChar] = PartyController.shadowPC.playerSkills.ultimateSkill;

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
        skillCDCounter[character] = skill[character].skillCooldown;

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

        activeUltimateSkillImage.sprite = skill[activeChar].skillIcon;
        activeUltimateSkillCDImage.sprite = skill[activeChar].skillIcon;
        activeUltimateSkillCDImage.fillAmount = skillCDCounter[activeChar] / skill[activeChar].skillCooldown;

        inactiveUltimateSkillImage.sprite = skill[inactiveChar].skillIcon;
        inactiveUltimateSkillCDImage.sprite = skill[inactiveChar].skillIcon;
        inactiveUltimateSkillCDImage.fillAmount = skillCDCounter[inactiveChar] / skill[inactiveChar].skillCooldown;
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
