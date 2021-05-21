using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for managing the Player's health
public class PlayerHealthManager : MonoBehaviour
{

    public int playerMaxHealth;             //The Player's maximum health points
    public int playerCurrentHealth;         //The Player's current health points

    private SpriteRenderer playerSprite;    //SpriteRenderer that renders the Player's sprite

    // Start is called before the first frame update
    void Start()
    {
        //Start with maximum amount of health
        SetMaxHealth();

        //Get a component reference to this object's SpriteRenderer
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //If Player's health falls to zero, Player dies
        if (playerCurrentHealth <= 0)
        {
            //Deactivate Player
            gameObject.SetActive(false);
        }
    }

    //Function for the player to take damage
    public void HurtPlayer(int damageToGive)
    {
        //Player loses health according to damage taken, but will not fall below zero
        playerCurrentHealth = Mathf.Max(playerCurrentHealth - damageToGive, 0);

        //Start coroutine to signal Player getting damaged
        StartCoroutine("HurtColor");
    }

    //Coroutine to signal Player getting damaged
    IEnumerator HurtColor()
    {
        for (int i = 0; i < 3; i++)
        {
            playerSprite.color = new Color(1f, 1f, 1f, 0.3f); //Red, Green, Blue, Alpha/Transparency
            yield return new WaitForSeconds(.1f);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    } //This IEnumerator runs 3 times, resulting in 3 flashes.

    //Function for the Player's health to be restored to maximum
    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
