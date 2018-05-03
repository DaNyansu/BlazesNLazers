using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCollider : MonoBehaviour {

     void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Shields are hit");
            Debug.Log(collision.name);
            Rigidbody enemycoll = collision.gameObject.GetComponent<Rigidbody>();
            //collision.gameObject.transform.Translate(collision.transform.position.x + 10f, collision.transform.position.y, collision.transform.position.z, Space.World);
        } 
    }

}
