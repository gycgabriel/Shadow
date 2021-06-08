using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the enemy to hurt the player
public class HurtPlayer : MonoBehaviour
{

    public int damageToGive;            //The base damage of the enemy
    private int currentDamage;          //The final damage to be dealt to the Player

    public GameObject damageNumber;     //The object that would display the damage number

    private Player thePlayer;    //The PlayerStats object in the scene
    private Rigidbody2D myRigidBody;        //The slime's Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        //Get a component reference to the scene's PlayerStats
        thePlayer = FindObjectOfType<Player>();
        //Get a component reference to this object's Rigidbody2D
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //myRigidBody.velocity = Vector2.zero;
        
        //Check if the enemy collided with the Player
        /*if (other.gameObject.name == "Player")
        {
            //If the enemy collided with the Player
            //Damage dealt is base damage of the enemy - the Player's defence, with a minimum of zero damage
            currentDamage = Mathf.Max(damageToGive - playerStats.currentDefence, 0);

            //Deal the damage to the Player by notifying the PlayerHealthManager
            other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(currentDamage);

            //Generate the object displaying the damage number at where the Player's position
            //Rotation set to zero to ensure damage number is upright
            GameObject clone = (GameObject) Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));

            //Set the damage number to be displayed
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }*/
    }
}
