using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : HurtBehaviour
{
    public GameObject gameOverCanvas;
    Player player;

    protected override void Start()
    {
        base.Start();
        player = (Player)creature;
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
    }

    public virtual void RecoverHP(int recoveryAmt)
    {
        player.currentHP = Mathf.Min(player.currentHP + recoveryAmt, player.getStats()["hp"]);
    }

    public virtual void RecoverMP(int recoveryAmt)
    {
        player.currentMP = Mathf.Min(player.currentMP + recoveryAmt, player.getStats()["mp"]);
    }


}
