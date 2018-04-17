using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCollider : MonoBehaviour {

     void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Shields are hit");
            Rigidbody enemycoll = collision.gameObject.GetComponent<Rigidbody>();
            enemycoll.AddForce(Vector3.up * 200,ForceMode.Impulse);
        } 
    }

}
