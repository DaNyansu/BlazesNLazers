using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDemoncontroller : MonoBehaviour {

    public int cDemonHp;
    int chargeDmg;

    GameObject chargeObject;

    public GameObject ceilProjectile;
    public Transform ceilSpawn;
    public float bulletspeed;


    // Use this for initialization
    void Start () {
        chargeObject = GameObject.Find("Lazer_Charge");
        StartCoroutine(ceilShoot());
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

    IEnumerator ceilShoot()
    {
        yield return new WaitForSeconds(1f);
        var bullet = (GameObject)Instantiate(ceilProjectile, ceilSpawn.position,ceilSpawn.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.up * bulletspeed);
        Destroy(bullet, 2.0f);
        StartCoroutine(ceilShoot());
    }
}
