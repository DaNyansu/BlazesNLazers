using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerMovement : MonoBehaviour {

    GameObject stagemanager;
    Transform lazerend;
    public float speed = 5;
    Rigidbody rb3d;
    bool moving;

	// Use this for initialization
	void Start () {
        rb3d = GetComponent<Rigidbody>();
        lazerend = GameObject.Find("Lazertrigger_End").transform;
        stagemanager = GameObject.Find("stageManager");
        moving = stagemanager.GetComponent<stageManagement>().lazermove;
	}
	
	// Update is called once per frame
	void Update () {
        moving = stagemanager.GetComponent<stageManagement>().lazermove;

    
        if(moving)
        {
            Debug.Log("we move");
            rb3d.isKinematic = false;
            Move();
        }

        else
        {
            rb3d.isKinematic = true;
        }

        if(transform.position.x >= lazerend.position.x)
        {
            //Laser has reaced the end
            // It has to stop
            speed = 0;
        }
	}

    void Move()
    {
        rb3d.velocity = transform.up * speed;
    }
}
