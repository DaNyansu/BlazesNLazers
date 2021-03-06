﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretController : MonoBehaviour {

    int turretHp;
    public int maxHp;
    int chargeDmg;

    GameObject chargeObject;
    stageManagement manager;

    public GameObject turretProjectile;
    public Transform turretSpawn;
    public float bulletspeed;

    bool shootingbool;

    // Use this for initialization
    void OnEnable () {
        turretHp = maxHp;
        chargeObject = GameObject.Find("Lazer_Charge");
        manager = FindObjectOfType<stageManagement>();


    }

    // Update is called once per frame
    void Update () {
        chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;

        Debug.DrawRay(transform.position, Vector3.left * 35, Color.green);

        checkforhit();

        if (turretHp <= 0)
        {
            gameObject.SetActive(false);
            manager.addscore(50);

        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "FreeBullet")
        {
            turretHp -= 5;
        }

        if (coll.gameObject.tag == "ChargeBullet")
        {
            turretHp -= chargeDmg;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Beam")
        {
            turretHp -= 2;
        }
    }

    void checkforhit()
    {
        LayerMask playerMask = LayerMask.GetMask("Player");



        if (Physics.Raycast(transform.position, Vector3.left, 35, playerMask, QueryTriggerInteraction.Collide) && !shootingbool)
        {
            Debug.Log("FOUND PLAYER");
            StartCoroutine(ceilShoot());
        }
    }

    IEnumerator ceilShoot()
    {
        shootingbool = true;
        yield return new WaitForSeconds(1f);
        var bullet = (GameObject)Instantiate(turretProjectile, turretSpawn.position, turretSpawn.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.up * -bulletspeed);
        Destroy(bullet, 6.0f);
        StartCoroutine(ceilShoot());
    }
}
