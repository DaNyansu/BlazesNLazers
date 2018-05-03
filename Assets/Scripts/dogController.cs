using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogController : MonoBehaviour {

    Rigidbody DogRB;
    Renderer DogRend;
    Animator enemyAnimator;

    int chargeDmg;

    public float speed;
    public float maxHp;
    float dogHp;

   GameObject chargeObject;
   public AudioSource dogAttack;

    stageManagement manager;

    bool stopmoving = false;

	// Use this for initialization
	void OnEnable () {
        dogHp = maxHp;
        DogRB = GetComponent<Rigidbody>();
        chargeObject = GameObject.Find("Lazer_Charge");
        enemyAnimator = GetComponent<Animator>();
        manager = FindObjectOfType<stageManagement>();
        stopmoving = false;


    }

    // Update is called once per frame
    void Update () {

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Vector3.left * 10, Color.green);
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
        LayerMask enemyMask = LayerMask.GetMask("Enemy");

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Vector3.left, 15, playerMask, QueryTriggerInteraction.Collide) && !dogAttack.isPlaying && dogHp > 0)
        {
            Debug.Log("FOUNDYOU");
            dogAttack.Play();
        }

        else if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Vector3.left, 15, enemyMask, QueryTriggerInteraction.Collide)  && dogHp > 0)
        {
            Debug.Log("enemyinfornt");
            enemyAnimator.SetTrigger("StopMove");
            stopmoving = true;
        }

        else
        {
            stopmoving = false;
            enemyAnimator.SetTrigger("StartMove");
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
        manager.addscore(50);
        gameObject.SetActive(false);
    }

    
}
