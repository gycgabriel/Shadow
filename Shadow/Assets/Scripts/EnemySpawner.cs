using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public GameObject enemySpawned = null;
    public float spawnTime;
    public bool isSpawning;
    public LayerMask blockingLayer;            // tilemap layers of non-passable objects

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAfterDelay(0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawned == null && !isSpawning)
        {
            StartCoroutine(SpawnAfterDelay(spawnTime));
        }
    }

    IEnumerator SpawnAfterDelay(float delayTime)
    {
        isSpawning = true;
        yield return new WaitForSeconds(delayTime);
        
        // If spawn location is clear of other units, then spawn. Otherwise, check again after 1s.
        while (!CheckIfSpawnLocationClear())
        {
            yield return new WaitForSeconds(1f);
        }
        
        enemySpawned = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        isSpawning = false;
    }

    bool CheckIfSpawnLocationClear()
    {
        Vector3 leftEdge = transform.position + new Vector3(-0.495f, 0f);
        Vector3 rightEdge = transform.position + new Vector3(0f, 0.495f);
        Vector3 topEdge = transform.position + new Vector3(0f, 0.495f);
        Vector3 btmEdge = transform.position + new Vector3(0f, -0.495f);

        // Create linecast from left edge to right edge and from top edge to btm edge of the intended spawn point like a "+".
        RaycastHit2D hitLeftToRight = Physics2D.Linecast(leftEdge, rightEdge, blockingLayer);
        RaycastHit2D hitTopToBtm = Physics2D.Linecast(topEdge, btmEdge, blockingLayer);

        // Spawn location is clear if both linecasts hit nothing
        return hitLeftToRight.transform == null && hitTopToBtm.transform == null;
    }
}
