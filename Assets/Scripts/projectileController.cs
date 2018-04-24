using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

     void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Ball")
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ball")
        {
            Destroy(gameObject);
        }
    }

}
