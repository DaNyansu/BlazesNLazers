using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformGenerator : MonoBehaviour {

    public GameObject interiorBlock;
    public Transform generationPoint;


    int generatedPlatforms = 0;
    int difficultyThreshold = 5;

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

        Debug.Log(generatedPlatforms);

        if(generatedPlatforms >= difficultyThreshold)
        {
            randomEnemyThreshold = randomEnemyThreshold - 7;
            difficultyThreshold +=5;
            
        }
		
        if(transform.position.x < generationPoint.transform.position.x)
        {
            
            transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y, transform.position.z);

            GameObject newBlock = theObjectPool.GetPooledObject();
            updatePlatforms(1);

            newBlock.transform.position = transform.position;
            newBlock.SetActive(true);

            if (Random.Range(0f, 100f) > randomEnemyThreshold)
            {
                theEnemyGenerator.spawnEnemies(new Vector3(transform.position.x + Random.Range(0f,20f), transform.position.y + 1f, transform.position.z - 5));
            }

        }
	}

    public void updatePlatforms(int amount)
    {
        generatedPlatforms = generatedPlatforms + amount;
    }

}
