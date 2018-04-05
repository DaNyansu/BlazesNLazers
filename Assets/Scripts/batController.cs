using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour {

    public int batDemonHp;
    int chargeDmg;

    public float amplitude;          
    public float speed;                  
    public float tempVal;
    Vector3 tempPos;

    Rigidbody batRb;

    GameObject chargeObject;

    // Use this for initialization
    void Start () {
        chargeObject = GameObject.Find("Lazer_Charge");
        batRb = GetComponent<Rigidbody>();
        tempVal = transform.position.y;
        tempPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;

        batRb.AddForce(transform.up * -speed);
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
        


        if (batDemonHp <= 0)
        {
            //gameObject.SetActive(false);
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
