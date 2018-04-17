using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{

    public GameObject pooledObject;

    public GameObject[] pooledArrayObject = new GameObject[5];

    public int pooledAmount;

    List<GameObject> pooledObjects;
    List<GameObject> waitingObjects;


    // Use this for initialization
    void Start()
    {
        pooledObjects = new List<GameObject>();
        waitingObjects = new List<GameObject>();

        if (gameObject.name == "EnemyPool")
        {
            for (int i = 0; i < pooledAmount; i++)
            {
                for (int p = 0; p < pooledArrayObject.Length; p++)
                {
                    GameObject obj = (GameObject)Instantiate(pooledArrayObject[p]);
                    obj.name = pooledArrayObject[p].name + i;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }

            }
        }

        else
        {
            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                obj.name = pooledObject.name + i;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    private void Update()
    {
        if (pooledObjects.Count <= 2)
        {
            movePooled();
        }

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                waitingObjects.Add(pooledObjects[i]);
                pooledObjects.RemoveAt(i);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public GameObject GetPooledEnemy()
    {
        // TÄMÄ RANDOMOI
        int i = Random.Range(0, pooledObjects.Count);
        if (!pooledObjects[i].activeInHierarchy)
        {
            return pooledObjects[i];
        }


        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    private void movePooled()
    {
        foreach (GameObject target in waitingObjects)
        {
            if (!target.activeInHierarchy)
            {
                waitingObjects.Remove(target);
                pooledObjects.Add(target);
            }

        }
    }
}
