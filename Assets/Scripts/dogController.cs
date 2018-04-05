using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogController : MonoBehaviour {

    Rigidbody DogRB;
    Renderer DogRend;

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
        
	}
	
	// Update is called once per frame
	void Update () {

        chargeDmg = chargeObject.GetComponent<chargeController>().ballDmg;
        
        if(!stopmoving)
        {
            move();
        }

        checkforhit();
      

        if(dogHp <= 0)
        {
            stopmoving = true;
            DogRB.isKinematic = true;
            transform.Rotate(Vector3.forward * 2, Space.World);
            DogRB.velocity = transform.right * 10 + transform.forward * 10;
            StartCoroutine(waitfordeath());
        }
	}

    void move()
    {
        DogRB.velocity = transform.up * -speed;
    }

    void checkforhit()
    {
        LayerMask playerMask = LayerMask.GetMask("Player");

        if (Physics.Raycast(transform.position, Vector3.left, 10, playerMask) && !dogAttack.isPlaying)
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
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    
}
