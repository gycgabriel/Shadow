using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorcererSpriteAnimation : PlayerSprite
{
    private PlayerController playerController;
    private SorcererSkills sorcererSkills;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        sorcererSkills = GetComponentInParent<SorcererSkills>();
    }

    public void CastFireball()
    {
        sorcererSkills.CastFireball();
    }

    public void CastManaBurst()
    {
        sorcererSkills.CastManaBurst();
    }

    public void StopAttack()
    {
        playerController.StopAttack();
    }
}
