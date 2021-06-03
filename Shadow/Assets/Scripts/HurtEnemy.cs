using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the weapon to damage the enemy
public class HurtEnemy : MonoBehaviour
{

    public int damageToGive;                //The base damage of this weapon
    private int currentDamage;              //The final damage to be dealt to the enemy
    public GameObject damageBurst;          //The blood spurt animation object
    public Transform hitPoint;              //The point where the weapon hits the enemy
    public GameObject damageNumber;         //The object that would display the damage number

    private PlayerStatsManager playerStatsManager;        //The PlayerStats object in the scene

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to the scene's PlayerStats
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the weapon has hit an enemy
        if (other.gameObject.tag == "Enemy")
        {
            //If this weapon hits an enemy,
            //Damage dealt is given by base damage of weapon + the Player's attack stat
            currentDamage = damageToGive + playerStatsManager.playerStats[(int) PlayerStatsManager.Stats.ATK];

            //Deal the damage to the enemy by notifying the EnemyHealthManager
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(currentDamage);

            //Generate the blood spurt animation at the hit point
            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);

            //Generate the object displaying the damage number at the hit point
            //Rotation set to zero to ensure damage number is upright
            GameObject clone = (GameObject) Instantiate(damageNumber, hitPoint.position, Quaternion.Euler(Vector3.zero));

            //Set the damage number to be displayed
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }
}
