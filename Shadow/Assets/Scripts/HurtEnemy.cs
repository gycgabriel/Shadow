using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the weapon to damage the enemy             // SCRIPTABLE OBJECT??
public class HurtEnemy : MonoBehaviour
{

    public int damageToGive;                // The base damage of this weapon
    private int currentDamage;              // The final damage to be dealt to the enemy
    public GameObject damageBurst;          // The blood spurt animation object
    public Transform hitPoint;              // The point where the weapon hits the enemy
    public GameObject damageNumber;         // The object that would display the damage number
    public bool isProjectile;               // Whether this object is a weapon or a projectile

    public Player thePlayer;                // or assign when instantiating fireball prefab
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only deal damage when hitting the enemy's Units collider, not its Obstacle collider
        if (other.gameObject.tag == "Enemy" && other.gameObject.layer == LayerMask.NameToLayer("Units"))
        {
            // If this weapon hits an enemy,
            // The damage dealt will be added by the Player's atk stat
            currentDamage = damageToGive + thePlayer.getStats()["atk"];

            // Deal the damage to the enemy by notifying the EnemyHealthManager
            bool attackSuccessful = other.gameObject.GetComponentInParent<HurtBehaviour>().hurt(currentDamage);

            if (attackSuccessful)
            {
                // Generate the blood spurt animation at the hit point
                Instantiate(damageBurst, other.transform.position, Quaternion.Euler(Vector3.zero));

                // Generate the object displaying the damage number at the hit point
                // Rotation set to zero to ensure damage number is upright
                GameObject clone = (GameObject) Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));

                // Set the damage number to be displayed
                clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
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
