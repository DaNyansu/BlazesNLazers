using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slerper : MonoBehaviour
{


    public float journeyTime;
    public float speed;

    bool wallpos = false;
    bool freepos = false;

    bool moving = false;
    float startTime;

    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;

    public Transform[] Balls = new Transform[4];
    public Transform[] Wall = new Transform[4];
    public Transform[] Free = new Transform[4];

    Transform[] startPos = new Transform[4];
    Transform[] endPos = new Transform[4];

    private void Start()
    {
        startPos = Balls;
        endPos = Free;
    }

    // Update is called once per frame

    void Update()
    {
        startPos = Balls;

        if (Input.GetKeyUp(KeyCode.E))
        {

            if (wallpos == false)
            {
                freepos = false;
                wallpos = true;
                startTime = Time.time;
                endPos = Wall;

            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if(freepos == false)
            {
                wallpos = false;
                startTime = Time.time;
                endPos = Free;
                freepos = true;
            }
        }

       if(freepos|| wallpos == true)
        {
            StartCoroutine("orbMovement");
        }
     

     

       
    }



    IEnumerator orbMovement()
    {
        for (int i = 0; i <4; i++)
        {
           
                float fracComplete = (Time.time - startTime) / journeyTime * speed;
                centerPoint = (startPos[i].position + endPos[i].position) * .5f;
                centerPoint -= Vector3.forward;
                startRelCenter = startPos[i].position - centerPoint;
                endRelCenter = endPos[i].position - centerPoint;
                Balls[i].transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
                Balls[i].transform.position += centerPoint;
            
        }
        moving = false;
        yield return null;
    }
    }
