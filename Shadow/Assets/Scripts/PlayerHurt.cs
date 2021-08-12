using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : HurtBehaviour
{
    public GameObject gameOverCanvas;
    Player player;

    public float regenInterval;
    private float regenCounter;
    public GameObject regenEffectHP;
    public GameObject regenEffectMP;

    protected override void Start()
    {
        base.Start();
        player = (Player)creature;
    }

    private void OnEnable()
    {
        // Make sure invincibility is not retained when hurt animation is interrupted.
        isInvincible = false;
    }

    protected override void Update()
    {
        if (player.isDead)
        {
            Debug.Log("Player Died. Game Over.");
            // Game Over for the Player
            isInvincible = false;
            Instantiate(gameOverCanvas);
            gameObject.SetActive(false);
        }

        regenCounter -= Time.deltaTime;
        if (regenCounter <= 0)
        {
            PassiveRegenOverTime();
            regenCounter = regenInterval;
        }
    }

    public virtual void RecoverHP(int recoveryAmt)
    {
        player.currentHP = Mathf.Min(player.currentHP + recoveryAmt, player.getStats()["hp"]);
    }

    public virtual void RecoverMP(int recoveryAmt)
    {
        player.currentMP = Mathf.Min(player.currentMP + recoveryAmt, player.getStats()["mp"]);
    }

    void PassiveRegenOverTime()
    {
        if (player.currentHP < player.getStats()["hp"])
        {
            RecoverHP(Mathf.FloorToInt(0.1f * player.getStats()["hp"]));
            GameObject hpEffect = Instantiate(regenEffectHP, this.transform.position, Quaternion.Euler(Vector3.zero));
            hpEffect.transform.parent = PartyController.activePC.transform;
        }

        if (player.currentMP < player.getStats()["mp"])
        {
            RecoverMP(Mathf.FloorToInt(0.1f * player.getStats()["mp"]));
            GameObject mpEffect = Instantiate(regenEffectMP, this.transform.position, Quaternion.Euler(Vector3.zero));
            mpEffect.transform.parent = PartyController.activePC.transform;
        }
            
        
    }

    public override bool Hurt(int damageToGive)
    {
        AudioManager.scriptInstance.PlaySFX("playerhurt");
        return base.Hurt(damageToGive);
    }

}
