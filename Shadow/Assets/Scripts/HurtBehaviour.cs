using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBehaviour : MonoBehaviour
{
    public GameObject creatureGO;                     
    protected Creature creature;                      // Enemy or Player
    protected SpriteRenderer[] sprites;
    
    public bool hasInvincibility;
    [System.NonSerialized]
    public bool isInvincible = false;


    protected virtual void Start()
    {
        if (creatureGO != null)
        {
            creature = creatureGO.GetComponent<Creature>();
            sprites = creatureGO.GetComponentsInChildren<SpriteRenderer>();
        } 
        else
        {
            creature = GetComponent<Creature>();
            sprites = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    protected virtual void Update()
    {
        if (creature.isDead)
        {
            gameObject.SetActive(false);
            // TODO: game over screen if player
        }
    }

    // returns whether the creature was successfully hurt
    public virtual bool hurt(int damageToGive)
    {
        // if the unit has invincibility frames and is invincible now, no damage will be taken
        if (hasInvincibility && isInvincible)
        {
            return false;
        }

        creature.currentHP = Mathf.Max(creature.currentHP - damageToGive, 0);        // creature health will not fall below zero
        StartCoroutine("hurtEffect");
        return true;
    }

    protected IEnumerator hurtEffect()            // change color to show hurt
    {
        isInvincible = true;
        for (int i = 0; i < 3; i++)     // runs 3 times, 3 flashes
        {
            Color[] originalColors = new Color[sprites.Length]; 
            for (int j = 0; j < sprites.Length; j++)
            {
                originalColors[j] = sprites[j].color;
                sprites[j].color = new Color(originalColors[j].r, originalColors[j].g, originalColors[j].b, 0.3f);         //Red, Green, Blue, Alpha/Transparency
            }
            yield return new WaitForSeconds(.1f);

            for (int j = 0; j < sprites.Length; j++)
            {
                sprites[j].color = new Color(originalColors[j].r, originalColors[j].g, originalColors[j].b, 1f);
            }
            yield return new WaitForSeconds(.1f);
        }
        isInvincible = false;
    }
}
