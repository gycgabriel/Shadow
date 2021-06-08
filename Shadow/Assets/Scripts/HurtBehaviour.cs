using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBehaviour : MonoBehaviour
{
    public GameObject creatureGO;                     
    private Creature creature;                      // Enemy or Player
    private SpriteRenderer sprite;


    void Start()
    {
        if (creatureGO != null)
        {
            creature = creatureGO.GetComponent<Creature>();
            sprite = creatureGO.GetComponent<SpriteRenderer>();
        } 
        else
        {
            creature = GetComponent<Creature>();
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (creature.isDead)
        {
            gameObject.SetActive(false);
            // TODO: game over screen if player
        }
    }

    public void hurt(int damageToGive)
    {
        creature.currentHP = Mathf.Max(creature.currentHP - damageToGive, 0);        // player health will not fall below zero
        StartCoroutine("hurtEffect");
    }

    IEnumerator hurtEffect()            // change color to show hurt
    {
        for (int i = 0; i < 3; i++)     // runs 3 times, 3 flashes
        {
            sprite.color = new Color(1f, 1f, 1f, 0.3f);         //Red, Green, Blue, Alpha/Transparency
            yield return new WaitForSeconds(.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }
}
