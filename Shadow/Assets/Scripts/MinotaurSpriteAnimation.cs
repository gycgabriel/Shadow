using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurSpriteAnimation : EnemySpriteAnimation
{
    public Animator animator;
    public Transform waveFirePoint;          // The point where the impact wave will be generated at
    public Vector3 waveTargetLocation;
    public GameObject impactWavePrefab;      // The impact wave object to be launched
    public GameObject fallingRocksPrefab;
    public int numOfFallingRocks;
    public int fallingRocksRange;
    public List<SpriteRenderer> spritesToBerserk;

    /**
     * Method to set target for Minotaur's Impact Wave
     */
    public void SetImpactWaveTarget(AnimationEvent ae)
    {
        AudioManager.scriptInstance.PlaySFX("minowaveshout");
        if (ae.animatorClipInfo.weight > 0.5f)
        {
            waveTargetLocation = enemyController.player.transform.position;
        }
    }

    /**
     * Method to instantiate Impact Wave upon Minotaur's smash attack
     */
    public void ReleaseImpactWave(AnimationEvent ae)
    {
        if (ae.animatorClipInfo.weight > 0.5f)
        {
            GameObject impactWave = Instantiate(impactWavePrefab, waveFirePoint.position, Quaternion.Euler(Vector3.zero));
            impactWave.GetComponentInChildren<Projectile>().SetDirection(waveTargetLocation - waveFirePoint.position);
            impactWave.GetComponentInChildren<HurtPlayer>().attackingEnemy = enemyController.GetComponent<Enemy>();

            // Match impact wave's color with Minotaur's sprite
            foreach (SpriteRenderer sprite in impactWave.GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color = spritesToBerserk[1].color;
            }
        }
    }

    /**
     * Method to create Falling Rocks
     */
    public void SummonFallingRocks(AnimationEvent ae)
    {
        AudioManager.scriptInstance.PlaySFX("minorubbleshout");
        if (ae.animatorClipInfo.weight > 0.5f)
        {
            List<Vector3> fallingRockLocations = GenerateFallingRockLocations(numOfFallingRocks);
        
            for (int i = 0; i < numOfFallingRocks; i++)
            {
                GameObject fallingRock = Instantiate(fallingRocksPrefab, fallingRockLocations[i], Quaternion.Euler(Vector3.zero));
                fallingRock.GetComponentInChildren<HurtPlayer>().attackingEnemy = enemyController.GetComponent<Enemy>();
            }
        }
        
    }

    List<Vector3> GenerateFallingRockLocations(int nRocks)
    {
        List<Vector3> fallingRockLocations = new List<Vector3>();
        List<Vector3> possibleLocations = new List<Vector3>();

        // Limits of Minotaur's range considering its transform is on its btm right quarter of the sprite.
        int leftLimit = -fallingRocksRange;
        int rightLimit = fallingRocksRange - 1;
        int topLimit = fallingRocksRange;
        int btmLimit = -fallingRocksRange - 1;

        for (int i = leftLimit; i <= rightLimit; i++)
        {
            for (int j = btmLimit; j <= topLimit; j++)
            {
                possibleLocations.Add(new Vector3(i, j, 0));
            }
        }

        // Falling Rocks won't land on Minotaur's locations
        possibleLocations.Remove(new Vector3(0, 0, 0));
        possibleLocations.Remove(new Vector3(0, 1, 0));
        possibleLocations.Remove(new Vector3(-1, 0, 0));
        possibleLocations.Remove(new Vector3(-1, 1, 0));

        Vector3 enemyPos = enemyController.transform.position;
        Vector3 playerPos = enemyController.player.transform.position;
        
        // First location will always target the player
        Vector3 location = new Vector3(playerPos.x, playerPos.y, 0);
        fallingRockLocations.Add(location);
        possibleLocations.Remove(location - enemyPos);

        // Fill up the remaining locations
        while (fallingRockLocations.Count < nRocks)
        {
            location = possibleLocations[Random.Range(0, possibleLocations.Count - 1)];
            fallingRockLocations.Add(location + enemyPos);
            possibleLocations.Remove(location);
        }

        return fallingRockLocations;
    }

    public void AddBerserkEffect(AnimationEvent ae)            // change color to show hurt
    {
        AudioManager.scriptInstance.PlaySFX("minoberserk");
        if (ae.animatorClipInfo.weight > 0.5f)
        {
            foreach (SpriteRenderer sprite in spritesToBerserk)
            {
                // Increase reddish hue of each sprite
                Color.RGBToHSV(sprite.color, out _, out float saturation, out _);
                sprite.color = Color.HSVToRGB(0, saturation + 0.20f, 1);
            }
        }
    }

}
