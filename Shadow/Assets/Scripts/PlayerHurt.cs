using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : HurtBehaviour
{
    Player player;

    protected override void Start()
    {
        base.Start();
        player = (Player)creature;
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
