using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurSpriteAnimation : EnemySpriteAnimation
{
    public Animator animator;
    public Transform waveFirePoint;          // The point where the impact wave will be generated at    
    public GameObject impactWavePrefab;      // The impact wave object to be launched

    /**
     * Coroutine to summon fireball, Sorcerer's normal attack
     */
    public void ReleaseImpactWave()
    {
        GameObject impactWave = Instantiate(impactWavePrefab, waveFirePoint.position, Quaternion.Euler(Vector3.zero));
        impactWave.GetComponentInChildren<Projectile>().SetDirection(
            enemyController.player.transform.position - waveFirePoint.position);
        impactWave.GetComponentInChildren<HurtPlayer>().attackingEnemy = enemyController.GetComponent<Enemy>();
    }

}
