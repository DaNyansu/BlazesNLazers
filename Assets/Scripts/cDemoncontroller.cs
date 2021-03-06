﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDemoncontroller : MonoBehaviour {

    int cDemonHp;
    public int maxHp;
    int chargeDmg;

    stageManagement manager;
    Animator ceilanimator;

    GameObject chargeObject;

    public GameObject ceilProjectile;
    public Transform ceilSpawn;
    public float bulletspeed;


    // Use this for initialization
    void OnEnable () {
        ceilanimator = GetComponent<Animator>();
        cDemonHp = maxHp;
        chargeObject = GameObject.Find("Lazer_Charge");
        manager = FindObjectOfType<stageManagement>();

        StartCoroutine(ceilShoot());
    }
	
	// Update is called once per frame
	void Update () {
        chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;

        if(cDemonHp <= 0)
        {
            manager.addscore(50);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Beam")
        {
            cDemonHp -= 2;
        }
    }

    IEnumerator ceilShoot()
    {
        yield return new WaitForSeconds(1f);
        ceilanimator.SetTrigger("Spit");
        var bullet = (GameObject)Instantiate(ceilProjectile, ceilSpawn.position,ceilSpawn.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.up * bulletspeed);
        Destroy(bullet, 2.0f);
        StartCoroutine(ceilShoot());
    }
}
