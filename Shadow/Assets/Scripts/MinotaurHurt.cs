using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurHurt : HurtBehaviour
{
    private MinotaurAI minotaurAI;

    protected override void Start()
    {
        base.Start();
        minotaurAI = GetComponent<MinotaurAI>();
    }

    protected override void Update()
    {
        if (creature.isDead)
        {
            Destroy(gameObject);
        }
    }
}
