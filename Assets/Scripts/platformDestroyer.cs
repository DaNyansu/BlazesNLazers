using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformDestroyer : MonoBehaviour {
    public GameObject platformDestructionPoint;

	// Use this for initialization
	void Start () {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(gameObject.name + gameObject.activeSelf);
		if(transform.position.x < platformDestructionPoint.transform.position.x && gameObject.activeSelf)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
	}
}
