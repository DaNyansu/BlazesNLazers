using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerMovement : MonoBehaviour {

    GameObject stagemanager;
    public float speed = 5;
    Rigidbody rb3d;
    bool moving;

	// Use this for initialization
	void Start () {
        rb3d = GetComponent<Rigidbody>();
        stagemanager = GameObject.Find("stageManager");
        moving = stagemanager.GetComponent<stageManagement>().lazermove;
	}
	
	// Update is called once per frame
	void Update () {
        moving = stagemanager.GetComponent<stageManagement>().lazermove;

    
        if(moving)
        {
            Debug.Log("we move");
            Move();
        }
	}

    void Move()
    {
        rb3d.velocity = transform.right * speed;
    }
}
