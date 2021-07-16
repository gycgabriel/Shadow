 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : Singleton<PartyController>
{
    public static bool shadowActive = false;
    public static GameObject player;
    public static GameObject shadow;

    public static Player playerP;
    public static Player shadowP;
    public static PlayerController playerPC;
    public static PlayerController shadowPC;
    public static PlayerController activePC;
    public static PlayerController inactivePC;

    public static SkillsUIManager skillsUIManager;
    public SkillsUIManager skillsUIManagerPrefab;

    public static QuestChain questChain;
    public static Quest quest;

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;

        if (DialogueManager.scriptInstance.dialogueBox.activeSelf)
        {
            bool skipInput = Input.GetKey(KeyCode.LeftControl);     // hold down to skip
            bool attackInput = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J);

            Singleton<DialogueManager>.scriptInstance.SkipDialogue(skipInput);

            if (attackInput)
            {
                Singleton<ScenarioManager>.scriptInstance.ContinueText();
            }
            return;     // no action while dialogue open
        }

        if (transform.childCount <= 0)
            return;

        // init
        if (player == null || shadow == null)
        {
            player = transform.GetChild(0).gameObject;
            shadow = transform.GetChild(1).gameObject;
        }

        if (skillsUIManager == null)
        {
            skillsUIManager = SkillsUIManager.scriptInstance;
        }

        if (playerP == null || shadowP == null)
        {
            playerP = player.GetComponent<Player>();
            shadowP = shadow.GetComponent<Player>();
        }
        if (playerPC == null || shadowPC == null)
        {
            playerPC = player.GetComponent<PlayerController>();
            shadowPC = shadow.GetComponent<PlayerController>();
        }
        else
        {
            if (shadowActive)
            {
                activePC = shadowPC;
                inactivePC = playerPC;
            }
            else
            {
                activePC = playerPC;
                inactivePC = shadowPC;
            }

            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            bool attackInput = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J);
            
            bool dashInput = Input.GetKey(KeyCode.LeftShift);
            bool ultimateInput = Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.U);
            bool switchToShadowInput = Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Q);

            inactivePC.SetPosition(activePC.transform.position, activePC.lastMove);
            activePC.Dash(dashInput);
            activePC.HandleInput(movement, attackInput, ultimateInput, switchToShadowInput);
            
        }

    }

    public static void SwitchShadow()
    {
        if (shadowActive)
        {
            shadow.SetActive(false);
            player.SetActive(true);
            activePC = playerPC;
            inactivePC = shadowPC;
        }
        else
        {
            shadow.SetActive(true);
            player.SetActive(false);
            activePC = shadowPC;
            inactivePC = playerPC;
        }
        shadowActive = !shadowActive;
    }

    public void Initialize(GameObject playerGO, GameObject shadowGO)
    {
        PartyController.player = playerGO;
        PartyController.shadow = shadowGO;
        PartyController.playerPC = playerGO.GetComponent<PlayerController>();
        PartyController.shadowPC = shadowGO.GetComponent<PlayerController>();
        PartyController.activePC = playerGO.GetComponent<PlayerController>();
        PartyController.inactivePC = shadowGO.GetComponent<PlayerController>();

        if (SkillsUIManager.scriptInstance == null)
        {
            skillsUIManager = Instantiate(skillsUIManagerPrefab);
        }
        else
        {
            skillsUIManager = SkillsUIManager.scriptInstance;
        }
    }


    public override void Destroy()
    {
        playerPC.Destroy();
        shadowPC.Destroy();
    }

    public static void AddExperience(int value)
    {
        playerP.AddExperience(value);
        shadowP.AddExperience(value);
    }

    // Questing
    public static void EnemyKilled(string tag)
    {
        if (quest != null && quest.isActive)
        {
            quest.goal.EnemyKilled(tag);
            if (quest.goal.IsReached())
            {
                AddExperience(quest.expReward);
                // add gold
                QuestWindow.scriptInstance.OpenCompleted(quest, questChain);
            }
        }
    }
}
