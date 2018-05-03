using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour {

    public objectPooler enemyPool;

    public float distanceBetweenEnemies;

    float spawnThreshold;

    platformGenerator generator;

    private void Start()
    {
        generator = FindObjectOfType<platformGenerator>();
    }

    public void spawnEnemies(Vector3 startPosition)
    {
        spawnThreshold = generator.randomEnemyThreshold;

        GameObject enemy1 = enemyPool.GetPooledEnemy();
        enemy1.transform.position = new Vector3(startPosition.x,enemy1.transform.position.y,enemy1.transform.position.z);
        enemy1.SetActive(true);

        if(Random.Range(0f,100f) > spawnThreshold)
        {
            GameObject enemy2 = enemyPool.GetPooledEnemy();
            enemy2.transform.position = new Vector3(startPosition.x + distanceBetweenEnemies, enemy2.transform.position.y, enemy2.transform.position.z);
            enemy2.SetActive(true);
        }
     
    }
}
