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
        //Debug.Log("Hit " + other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            // If this weapon hits an enemy,
            // The damage dealt will be added by the Player's atk stat
            currentDamage = damageToGive + thePlayer.getStats()["atk"];

            // Deal the damage to the enemy by notifying the EnemyHealthManager
            bool attackSuccessful = other.gameObject.GetComponentInParent<HurtBehaviour>().hurt(currentDamage);

            if (attackSuccessful)
            {
                // Generate the blood spurt animation at the hit point
                Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);

                // Generate the object displaying the damage number at the hit point
                // Rotation set to zero to ensure damage number is upright
                GameObject clone = (GameObject) Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));

                // Set the damage number to be displayed
                clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
            }

        }

        if (other.gameObject.tag != "Player" && isProjectile)
        {
            if (other.gameObject.tag != "Enemy")
            {
                Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
            }
            Destroy(gameObject);
        }
    }
}
