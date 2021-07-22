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
    public static ItemHotkeyUIManager itemHotkeyUIManager;

    public static QuestChain questChain;
    public static Quest quest;

    public static Inventory inventory;

    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;

        if (DialogueManager.scriptInstance.dialogueBox.activeSelf)
        {
            bool skipInput = Input.GetKey(KeyCode.LeftControl);     // hold down to skip
            bool attackInput = Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J);

            DialogueManager.scriptInstance.SkipDialogue(skipInput);

            if (attackInput)
            {
                ScenarioManager.scriptInstance.ContinueText();
            }
            return;     // no action while dialogue open
        }

        if (QuestWindow.scriptInstance != null && QuestWindow.scriptInstance.isOpen)
            return;

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

        if (itemHotkeyUIManager == null)
        {
            itemHotkeyUIManager = ItemHotkeyUIManager.scriptInstance;
        }

        if (inventory == null)
        {
            inventory = new Inventory { Gold = 0 };
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
            bool ultimateInput = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.V);
            bool switchToShadowInput = Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Q);
            bool[] hotkeyInputs = new bool[4] { 
                Input.GetKeyDown(KeyCode.Alpha1), 
                Input.GetKeyDown(KeyCode.Alpha2) , 
                Input.GetKeyDown(KeyCode.Alpha3) , 
                Input.GetKeyDown(KeyCode.Alpha4) };

            inactivePC.SetPosition(activePC.transform.position, activePC.lastMove);
            activePC.Dash(dashInput);
            activePC.HandleInput(movement, attackInput, ultimateInput, switchToShadowInput, hotkeyInputs);
            
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
        shadowActive = false;
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
        itemHotkeyUIManager = ItemHotkeyUIManager.scriptInstance;

        inventory = new Inventory { Gold = 0 };
    }

    public static void AddExperience(int value)
    {
        playerP.AddExperience(value);
        shadowP.AddExperience(value);
    }

    public static void AddGold(int amt)
    {
        Debug.Log("Added " + amt + " Gold");
        inventory.Gold += amt;
    }

    public void Respawn(float expLoss)
    {
        playerP.isDead = false;
        shadowP.isDead = false;
        playerP.currentExp = (int)((1f - expLoss) * playerP.currentExp);
        shadowP.currentExp = (int)((1f - expLoss) * shadowP.currentExp);
    }

    public void FullRestore()
    {
        playerP.currentHP = playerP.getStats()["hp"];
        playerP.currentMP = playerP.getStats()["mp"];

        shadowP.currentHP = shadowP.getStats()["hp"];
        shadowP.currentMP = shadowP.getStats()["mp"];
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
                AddGold(quest.goldReward);
                QuestWindow.scriptInstance.OpenCompleted(quest, questChain);
            }
        }
    }

    // amt is total amount in inventory
    public static void ItemGet(string tag, int amt)
    {
        if (quest != null && quest.isActive)
        {
            quest.goal.ItemGet(tag, amt);
            if (quest.goal.IsReached())
            {
                AddExperience(quest.expReward);
                AddGold(quest.goldReward);
                QuestWindow.scriptInstance.OpenCompleted(quest, questChain);
            }
        }
    }

    public void MovePlayer(Vector2 movement)
    {
        activePC.HandleInput(movement, false, false, false, new bool[4] { false, false, false, false });
        inactivePC.HandleInput(movement, false, false, false, new bool[4] { false, false, false, false });
    }

}
