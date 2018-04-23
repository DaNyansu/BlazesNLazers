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
            enemycoll.AddForce(Vector3.right * 1000, ForceMode.Impulse);
        } 
    }

}
