using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sizer : MonoBehaviour {
    public float maxSize;
    public float growSpeed;

    int ballDmg;
    int dmgMultiplier = 2;

    Rigidbody rb;

    float timer;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(timer + "ja" + ballDmg);

        if(Input.GetKey(KeyCode.Mouse0))
        {
            timer = 0;
            ballDmg = 1;
            if(transform.localScale.x < maxSize)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growSpeed;
                ballDmg = ballDmg * Mathf.RoundToInt(Time.time) * dmgMultiplier;
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            release();
          
        }
   
    }

    void release()
    {
        rb.AddForce(Vector3.left * 10);
    }
}
