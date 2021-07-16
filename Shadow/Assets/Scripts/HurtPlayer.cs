using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the enemy to hurt the player
public class HurtPlayer : MonoBehaviour
{
    public AttackInfo attackInfo;       // Scriptable Object containing the info of the attack
    private int currentDamage;          // The final damage to be dealt to the Player

    public GameObject damageNumber;     // The object that would display the damage number
    public Enemy attackingEnemy;        // The enemy that is executing the attack

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentDamage = attackInfo.CalculateDamage(attackingEnemy, other.GetComponentInParent<Player>());

            bool attackSuccessful = other.gameObject.GetComponentInParent<HurtBehaviour>().Hurt(currentDamage);

            // Check if attack was successful or Player was invincible
            if (attackSuccessful)
            {
                // Display damage numbers
                GameObject clone = (GameObject) Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
            }
            
        }
    }
}
