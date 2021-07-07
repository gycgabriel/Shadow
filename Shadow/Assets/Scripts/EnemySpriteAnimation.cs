using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteAnimation : MonoBehaviour
{
    public EnemyAI enemyController;

    void StopAttack()
    {
        enemyController.StopAttack();
    }

}
