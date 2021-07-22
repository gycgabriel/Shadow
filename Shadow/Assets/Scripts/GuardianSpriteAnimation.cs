using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianSpriteAnimation : PlayerSprite
{
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void StopAttack()
    {
        playerController.StopAttack();
    }
}
