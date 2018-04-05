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
            
            //var block = (GameObject)Instantiate(interiorBlock, transform.position, interiorBlock.transform.rotation);
            transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y, transform.position.z);

            if (Random.Range(0f, 100f) > randomEnemyThreshold)
            {
                Debug.Log("Spawning enemies");
                theEnemyGenerator.spawnEnemies(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 5));
            }

            GameObject newBlock = theObjectPool.GetPooledObject();

            newBlock.transform.position = transform.position;
            newBlock.SetActive(true);

        }
	}
}
