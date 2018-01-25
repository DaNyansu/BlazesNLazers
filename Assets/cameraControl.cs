using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour {

    Vector3 offset;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");

        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        transform.position = player.transform.position + offset;
	}
}
