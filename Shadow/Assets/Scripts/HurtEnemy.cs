using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the attack to damage the enemy             // SCRIPTABLE OBJECT??
public class HurtEnemy : MonoBehaviour
{
    public AttackInfo attackInfo;           // Scriptable Object containing the info of the attack
    private int currentDamage;              // The final damage to be dealt to the enemy
    public GameObject damageBurst;          // The blood spurt animation object
    public Transform hitPoint;              // The point where the weapon hits the enemy
    public GameObject damageNumber;         // The object that would display the damage number
    public bool isProjectile;               // Whether this object is a weapon or a projectile

    public Player thePlayer;                // or assign when instantiating fireball prefab

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only deal damage when hitting the enemy's Units collider, not its Obstacle collider
        if (other.gameObject.tag == "Enemy" && other.gameObject.layer == LayerMask.NameToLayer("Units"))
        {
            currentDamage = attackInfo.CalculateDamage(thePlayer, other.GetComponentInParent<Enemy>());

            bool attackSuccessful = other.gameObject.GetComponentInParent<HurtBehaviour>().Hurt(currentDamage);

            // Check if attack was successful or the enemy was invincible
            if (attackSuccessful)
            {
                // Generate the blood spurt animation and damage numbers
                Instantiate(damageBurst, other.transform.position, Quaternion.Euler(Vector3.zero));
                GameObject damageNumbeGO = (GameObject) Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
                damageNumbeGO.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
            }

            if (isProjectile)
            {
                Destroy(gameObject);
            }
        }

        // If hit something else other than Player or Enemy, projectile is blocked
        else if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Enemy") && isProjectile)
        {
            Instantiate(damageBurst, this.transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(gameObject);
        }
    }


}
