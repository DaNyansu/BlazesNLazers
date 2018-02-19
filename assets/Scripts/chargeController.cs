using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeController : MonoBehaviour
{
    Rigidbody rb;

    float chargeSize;
    float timer = 1;

    [HideInInspector]
    public int ballDmg;
    float dmgMultiplier = 1.2f;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chargeSize = transform.localScale.x * 2;
        ballDmg = 1;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && rb.isKinematic)
        {
            timer += Time.deltaTime;

            if (transform.localScale.x < chargeSize)
            {
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 0.01f;
                if(ballDmg < 30)
                {
                    ballDmg = ballDmg + (Mathf.RoundToInt(timer) * Mathf.RoundToInt(dmgMultiplier));
                }

                if(ballDmg > 30)
                {
                    release();
                }
                
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            release();
        }

    }

    void release()
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.right * 1000);
        timer = 0;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Ball")
        {
            Destroy(gameObject);
        }

    }

}
