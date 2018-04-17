using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour {

    public objectPooler enemyPool;

    public float distanceBetweenEnemies;
    
    public void spawnEnemies(Vector3 startPosition)
    {
        GameObject enemy1 = enemyPool.GetPooledObject();
        enemy1.transform.position = new Vector3(startPosition.x,enemy1.transform.position.y,enemy1.transform.position.z);
        enemy1.SetActive(true);
    }
}
