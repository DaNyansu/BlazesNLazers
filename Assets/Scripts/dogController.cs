using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogController : MonoBehaviour {

    Rigidbody DogRB;
    Renderer DogRend;
    Animator enemyAnimator;

    int chargeDmg;

    public float speed;
    public float dogHp = 20;

    GameObject chargeObject;
   public AudioSource dogAttack;

    bool stopmoving = false;

	// Use this for initialization
	void Start () {
        DogRB = GetComponent<Rigidbody>();
        chargeObject = GameObject.Find("Lazer_Charge");
        enemyAnimator = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {

        chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;
        
        if(!stopmoving)
        {
            move();
            enemyAnimator.SetTrigger("StartMove");
        }

        checkforhit();
      

        if(dogHp <= 0)
        {
            stopmoving = true;
            enemyAnimator.SetTrigger("StopMove");
            transform.Rotate(Vector3.right * -2, Space.World);
            DogRB.velocity = transform.forward * - 10;
            StartCoroutine(waitfordeath());
        }
	}

    void move()
    {
        DogRB.velocity = transform.forward * speed;
    }

    void checkforhit()
    {
        LayerMask playerMask = LayerMask.GetMask("Player");

        if (Physics.Raycast(transform.position, Vector3.left, 10, playerMask, QueryTriggerInteraction.Collide) && !dogAttack.isPlaying && dogHp > 0)
        {
            dogAttack.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if(collision.gameObject.tag == "FreeBullet")
        {
            dogHp -= 5;
        }

        if (collision.gameObject.tag == "ChargeBullet")
        {
            dogHp -= chargeDmg;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Beam")
        {
            dogHp -= 2;
        }
    }

    IEnumerator waitfordeath()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    
}
