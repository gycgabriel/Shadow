using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public GameObject enemySpawned = null;
    public float spawnTime;
    public bool isSpawning;

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
        enemySpawned = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        isSpawning = false;
    }
}
