using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public int maxEnemyHp;
    int enemyHp;

    GameObject chargeObject;
    int chargeDmg;

    // Use this for initialization
    void OnEnable () {
        chargeObject = GameObject.Find("Lazer_Charge");
        enemyHp = maxEnemyHp;
    }

    // Update is called once per frame
    void Update () {
        chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;

        if (enemyHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "FreeBullet")
        {
            enemyHp -= 5;
        }

        if (coll.gameObject.tag == "ChargeBullet")
        {
            enemyHp -= chargeDmg;
        }
    }
}
