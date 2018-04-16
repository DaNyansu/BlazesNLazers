using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformGenerator : MonoBehaviour {

    public GameObject interiorBlock;
    public Transform generationPoint;

    private float platformWidth;

    public objectPooler theObjectPool;

    private enemyGenerator theEnemyGenerator;
    public float randomEnemyThreshold;

	// Use this for initialization
	void Start () {
        platformWidth = 28.27f;
        theEnemyGenerator = FindObjectOfType<enemyGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(transform.position.x < generationPoint.transform.position.x)
        {
            
            transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y, transform.position.z);

            GameObject newBlock = theObjectPool.GetPooledObject();

            newBlock.transform.position = transform.position;
            newBlock.SetActive(true);

            if (Random.Range(0f, 100f) > randomEnemyThreshold)
            {
                Debug.Log("Spawning enemies");
                theEnemyGenerator.spawnEnemies(new Vector3(transform.position.x + Random.Range(0f,20f), transform.position.y + 1f, transform.position.z - 5));
            }

        }
	}
}
