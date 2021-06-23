using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUIManager : Singleton<SkillsUIManager>
{
    public Image ultimateSkillImage;
    public Image ultimateSkillCDImage;

    // Player's skill 
    public Skill playerSkill;
    public float playerSkillCDCounter = 0;
    public bool isPlayerSkillCooldown = false;

    
    // Shadow's skill
    public Skill shadowSkill;
    public float shadowSkillCDCounter = 0;
    public bool isShadowSkillCooldown = false;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerSkill = PartyController.playerPC.playerSkills.ultimateSkill;
        shadowSkill = PartyController.shadowPC.playerSkills.ultimateSkill;

        if (isPlayerSkillCooldown)
        {
            playerSkillCDCounter -= Time.deltaTime;

            if (playerSkillCDCounter <= 0)
            {
                playerSkillCDCounter = 0;
                isPlayerSkillCooldown = false;
            }
        }

        if (isShadowSkillCooldown)
        {
            shadowSkillCDCounter -= Time.deltaTime;

            if (shadowSkillCDCounter <= 0)
            {
                shadowSkillCDCounter = 0;
                isShadowSkillCooldown = false;
            }
        }

        if (!PartyController.shadowActive)
        {
            ultimateSkillImage.sprite = playerSkill.skillIcon;
            ultimateSkillCDImage.sprite = playerSkill.skillIcon;
            ultimateSkillCDImage.fillAmount = playerSkillCDCounter / playerSkill.skillCooldown;
        }
        else
        {
            ultimateSkillImage.sprite = shadowSkill.skillIcon;
            ultimateSkillCDImage.sprite = shadowSkill.skillIcon;
            ultimateSkillCDImage.fillAmount = shadowSkillCDCounter / shadowSkill.skillCooldown;
        }
        
    }

    public void UseSkill()
    {
        if (!PartyController.shadowActive)
        {
            isPlayerSkillCooldown = true;
            playerSkillCDCounter = playerSkill.skillCooldown;
            ultimateSkillCDImage.fillAmount = 1;
        }
        else
        {
            isShadowSkillCooldown = true;
            shadowSkillCDCounter = shadowSkill.skillCooldown;
            ultimateSkillCDImage.fillAmount = 1;
        }
        
    }
}
