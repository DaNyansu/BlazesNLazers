using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDemoncontroller : MonoBehaviour {

    public int cDemonHp;
    int chargeDmg;

    GameObject chargeObject;

    // Use this for initialization
    void Start () {
        chargeObject = GameObject.Find("Lazer_Charge");
    }
	
	// Update is called once per frame
	void Update () {
        chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;

        if(cDemonHp <= 0)
        {
            gameObject.SetActive(false);
        }


        //SPAWNAA PALLUKOITA 2 SEC INTERVALLEISSA
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "FreeBullet")
        {
            cDemonHp -= 5;
        }

        if (coll.gameObject.tag == "ChargeBullet")
        {
            cDemonHp -= chargeDmg;
        }
    }
}
