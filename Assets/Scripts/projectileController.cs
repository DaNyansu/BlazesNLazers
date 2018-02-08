using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour {
    Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

     void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Ball")
        {
            Destroy(gameObject);
        }
        
    }

}
