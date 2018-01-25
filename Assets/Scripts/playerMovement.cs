using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    //----PLAYER MOVEMENT----

    public float movespeed;

    Rigidbody rb3d;


    //----BALL MOVEMENT----

    public float journeyTime;


    bool canmove;
    bool orbmoving = false;
    bool startmove;

    float startTime;

    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;



    public Transform[] Balls = new Transform[4];
    public Transform[] Wall = new Transform[4];
    public Transform[] Free = new Transform[4];
    public Transform[] Shield = new Transform[4];
    public Transform[] Lazer = new Transform[4];

    Transform[] startPos = new Transform[4];
    Transform[] endPos = new Transform[4];

    Vector3 startV;
    Vector3 EndV;
    Vector3 CurV;


    // Use this for initialization
    void Start()
    {

        rb3d = GetComponent<Rigidbody>();
        StartCoroutine("waitforstart");
        endPos = Free;
        startPos = Balls;
        canmove = true;
        orbmoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        startV = Balls[1].transform.position;
        EndV = endPos[1].transform.position;

        if (canmove == true)
        {
            Move();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartCoroutine("dash");
            }
        }


        if(!orbmoving)
        {
            //FREE
            if (Input.GetKeyUp(KeyCode.Q))
            {
                StopCoroutine(moveorbs());
                startPos = Balls;
                endPos = Free;
                startTime = Time.time;

            }

            //WALL
            if (Input.GetKeyUp(KeyCode.W))
            {
                StopCoroutine(moveorbs());
                startPos = Balls;
                endPos = Wall;
                startTime = Time.time;
            }

            //SHIELD
            if (Input.GetKeyUp(KeyCode.E))
            {
                StopCoroutine(moveorbs());
                startPos = Balls;
                endPos = Shield;
                startTime = Time.time;

            }

            //LAZER
            if (Input.GetKeyUp(KeyCode.R))
            {
                StopCoroutine(moveorbs());
                startPos = Balls;
                endPos = Lazer;
                startTime = Time.time;
            }
        }
     



        if (startV != EndV)
        {
            StartCoroutine(moveorbs());
        }

    }




    void Move()
    {
        rb3d.velocity = transform.right * movespeed;
    }


    private IEnumerator waitforstart()
    {
        yield return new WaitForSecondsRealtime(2);
        Move();
        canmove = true;
    }

    IEnumerator dash()
    {
        movespeed = 10;
        yield return new WaitForSeconds(0.3f);
        movespeed = 2.2f;
    }

    IEnumerator moveorbs()
    {
        orbmoving = true;
        Debug.Log("1." + orbmoving);
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("2." + orbmoving);
            float fracComplete = (Time.time - startTime) / journeyTime;
            centerPoint = (startPos[i].position + endPos[i].position) * 0.5f;
            centerPoint += Vector3.forward * 3f;
            startRelCenter = startPos[i].position - centerPoint;
            endRelCenter = endPos[i].position - centerPoint;
            Debug.Log("2.1." + orbmoving);
            Balls[i].transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete);
            Debug.Log("2.2" + orbmoving);
            Balls[i].transform.position += centerPoint;
        }
        Debug.Log("3." + orbmoving);
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("4. " + orbmoving);

        if(startV == EndV)
        {
            orbmoving = false;
        }
   
        
        yield return null;
    }


}
