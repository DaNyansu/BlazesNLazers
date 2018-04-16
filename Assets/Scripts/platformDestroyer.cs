using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformDestroyer : MonoBehaviour {
    public GameObject platformDestructionPoint;

	// Use this for initialization
	void Start () {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
	}

    private void OnEnable()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    // Update is called once per frame
    void LateUpdate () {

        if (transform.position.x < platformDestructionPoint.transform.position.x && gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
	}
}
