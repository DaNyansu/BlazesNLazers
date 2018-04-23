using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour {

    public int maxDemonHp;
    int batDemonHp;
    int chargeDmg;

    public float amplitude;          
    public float floatspeed;
    public float movespeed;
    public float tempVal;
    Vector3 tempPos;

    Rigidbody batRb;

    GameObject chargeObject;

    void OnEnable ()
    {
        batDemonHp = maxDemonHp;
        chargeObject = GameObject.Find("Lazer_Charge");
        batRb = GetComponent<Rigidbody>();
        tempVal = transform.position.y;
        tempPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;

        batRb.velocity =transform.up * -movespeed;
        tempPos.y = tempVal + amplitude * Mathf.Sin(floatspeed * Time.time);
        transform.position = new Vector3(transform.position.x, tempPos.y, transform.position.z);
       


        if (batDemonHp <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "FreeBullet")
        {
            batDemonHp -= 5;
        }

        if (coll.gameObject.tag == "ChargeBullet")
        {
            batDemonHp -= chargeDmg;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Beam")
        {
            batDemonHp -= 2;
        }
    }
}
