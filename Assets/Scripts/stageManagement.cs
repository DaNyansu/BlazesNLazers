using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManagement : MonoBehaviour {

    GameObject player;
    float playerposx;
    GameObject lazertrigger;
    float lazertriggerposx;

    public bool lazermove = false;

	// Use this for initialization
	void Start () {
        lazertrigger = GameObject.Find("Lazertrigger");
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        playerposx = player.transform.position.x;
        lazertriggerposx = lazertrigger.transform.position.x;
        

        if (playerposx >= lazertriggerposx)
        {
            lazermove = true;
            
        }
	}
}
