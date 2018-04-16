using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{

    //----PLAYER MOVEMENT----

    
    bool dashbool;
    public bool playerDied;

    GameObject playerColl;
    Transform endTrigger;

    public float movespeed;
    public float movemultiplier;
    public int dashCool;
    public float dashCd;
    public float dashCdRem;

    public Text dashText;

    Animator playerAnimator;
    Rigidbody rb3d;

    public AudioSource lasersound;
    public AudioSource footsteps;

    //----PLAYER VARIABLES----
    public float FreeSpeed;
    public float WallSpeed;


    //----BALL MOVEMENT----

    public float journeyTime;

    Quaternion OriginRot;

    bool canmove;
    bool orbmoving = false;

    float startTime;

    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;


    public Transform[] Balls = new Transform[4];
    public Transform[] Wall = new Transform[4];
    public Transform[] Free = new Transform[4];
    public Transform[] Shield = new Transform[4];
    public Transform[] Lazer = new Transform[4];

    public Camera cameraboi;

    Transform[] startPos = new Transform[4];
    Transform[] endPos = new Transform[4];

    Vector3 startV;
    Vector3 EndV;

    //--- SHOOTING ----
    public float bulletspeed;
    public float chargeSize;


    public int wallPAmount = 2;
    int[] chargeValue = new int[4];

    public GameObject lazerPrefab;
    public GameObject chargePrefab;

    public Transform[] FreeSpawn = new Transform[4];

    public bool allowfire = true;
    bool[] FreeColl = new bool[4];
    bool[] chargeReady = new bool[4];

    public GameObject beam;
    public GameObject shields;
    public GameObject wall;

    //--- HEATING ---
    int heating;
    int maxHeat = 100;
    int wallheat = 5;
    int freeheat = 2;

    bool coolingbool = false;
    bool heatingbool = false;
    bool overheatbool = false;
    bool paused = false;

    public Text heatText;
    Color normalcolor;


    // Use this for initialization
    void Start()
    {
        endPos = Free;
        startPos = Balls;
        //endTrigger = GameObject.Find("Playertrigger_End").transform;
        playerColl = GameObject.Find("PlayerColl");
        Debug.Log(normalcolor + "ja" + heatText.color);
        rb3d = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        StartCoroutine("waitforstart");
        OriginRot = Balls[0].rotation;
        dashbool = true;
        endPos = Free;
        startPos = Balls;
        orbmoving = false;
        heating = 0;
        normalcolor = heatText.color;
        updateHeat();
        updateDash();
        dashCdRem = dashCd;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDied = playerColl.GetComponent<playerCollisions>().playerDead;

        updateHeat();
        checkBallColl();


        // DEBUGGAUS
        Vector3 fwd = transform.TransformDirection(Vector3.up) * 10;
        //DEBUGGAUS LOPPUU


        startV = Balls[1].transform.position;
        EndV = endPos[1].transform.position;

        /*if(transform.position.x >= endTrigger.position.x)
        {
            movespeed = 0;
            playerAnimator.SetTrigger("PlayerStop");

        }
        */

        if(playerDied)
        {
            gameObject.SetActive(false);
        }

        if(overheatbool)
        {
            allowfire = false;
        }

        if(Time.timeScale == 0)
        {
            paused = true;
        }

        else
        {
            paused = false;
        }

        if (heating > maxHeat)
        {
            heating = maxHeat;
        }

        if(heating < 0)
        {
            heating = 0;
        }

        if (heating == maxHeat)
        {
            StartCoroutine(overheat());
            //On ylikuumentunut
        }

        if (heating > 0 && allowfire && !coolingbool)
        {
            StartCoroutine(cooling());
        }

        if (heating < maxHeat && !heatingbool)
        {
            StartCoroutine(heatingCoroutine());
        }

        if (startV == Free[1].transform.position)
        {
            rotateBalls();
        }

        if (endPos != Free)
        {
            resetorbRot();
        }

        if (startV == Lazer[1].transform.position)
        {
            LazerBeam();
        }

        if (startV == Shield[1].position && allowfire)
        {
            shields.SetActive(true);
        }
        else
        {
            shields.SetActive(false);
        }

        if (startV == Wall[1].position && !overheatbool)
        {
            wall.SetActive(true);
        }
        else
        {
            wall.SetActive(false);
        }

        if (canmove == true && !playerDied)
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }

            if (!orbmoving && !paused)
            {
                if (Input.GetKey(KeyCode.Mouse0) && (allowfire) && endPos == Free)
                {
                    freeShoot();
                }

                if (Input.GetKey(KeyCode.Mouse0) && (allowfire) && endPos == Wall)
                {
                    //Charging
                    wallShoot();
                }
            }

            Move();

            if (Input.GetKey(KeyCode.LeftShift) && dashbool)
            {
                heating = heating - dashCool;
                StartCoroutine("dash");
                
            }

        }

        if (!orbmoving)
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

        if (startV != EndV && allowfire)
        {
            StartCoroutine(moveorbs());
        }

    }


    void freeShoot()
    {
        allowfire = false;
        for (int i = 0; i < 4; i++)
        {
            if (!FreeColl[i])
            {
                var bullet = (GameObject)Instantiate(lazerPrefab, FreeSpawn[i].position, FreeSpawn[i].rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.up * bulletspeed);
                Destroy(bullet, 2.0f);
                if (heating <= maxHeat - freeheat)
                {
                    heating += 2;
                }
                lasersound.Play();
                updateHeat();
            }
        }
        StartCoroutine(FreeDelay());
    }

    void wallShoot()
    {
        allowfire = false;
        for (int i = 0; i < 4; i++)
        {
            if (chargeValue[i] == 0)
            {
                var bullet = (GameObject)Instantiate(chargePrefab, FreeSpawn[i].position, FreeSpawn[i].rotation, transform.parent = FreeSpawn[i]);
                Destroy(bullet, 3.0f);
                chargeValue[i]++;
                if (heating < maxHeat)
                {
                    heating += 2;
                }
            }
        }

        StartCoroutine(WallDelay());

    }

    void updateHeat()
    {
        heatText.text = "HEAT: " + heating.ToString();
        heatText.color = normalcolor;
        
        if(overheatbool)
        {
            heatText.text = "OVERHEAT";
            heatText.color = Color.red;
        }
    }

    void updateDash()
    {
        dashText.text = dashCdRem.ToString() + " :Dash";
        dashText.color = Color.red;

        if(dashbool)
        {
            dashText.text = "DASH READY";
            dashText.color = normalcolor;
        }
    }

    void LazerBeam()
    {

        if (Input.GetKey(KeyCode.Mouse0) && allowfire)
        {
            beam.SetActive(true);
            Debug.Log("laser is active");

        }

        else
        {
            beam.SetActive(false);
            allowfire = true;
        }

    }

    void resetorbRot()
    {
        for (int i = 0; i < 4; i++)
        {
            Balls[i].transform.rotation = Quaternion.Slerp(Balls[i].transform.rotation, OriginRot, 3f);
        }
    }

    void Move()
    {
            playerAnimator.SetTrigger("playerMove");
            rb3d.velocity = transform.right * movespeed;  
    }

    void rotateBalls()
    {
        if(Time.timeScale != 0)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 mousePosition;
                mousePosition = cameraboi.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cameraboi.transform.position.z));
                Balls[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);
            }
        }

    }

    void checkBallColl()
    {
        LayerMask ballMask = LayerMask.GetMask("Ball");
        Vector3 fwd = transform.TransformDirection(Vector3.up) * 20;
        float sphereRad = 0.2f;
        RaycastHit hit;

        for (int i = 0; i < 4; i++)
        {
            if (Physics.SphereCast(FreeSpawn[i].position, sphereRad, FreeSpawn[i].rotation * fwd, out hit, 10, ballMask.value))
            {
                FreeColl[i] = true;
            }

            else
            {
                FreeColl[i] = false;
            }
        }
    }

    private IEnumerator waitforstart()
    {
        yield return new WaitForSeconds(2);
        Move();
        canmove = true;
    }

    IEnumerator dash()
    {
        dashbool = false;
        StartCoroutine(dashCdCor());
        movespeed = movespeed * movemultiplier;
        yield return new WaitForSeconds(0.75f);
        movespeed = movespeed / movemultiplier;
    }

    IEnumerator dashCdCor()
    {
        updateDash();
        while (dashCdRem >= 0)
        {
            dashCdRem--;
            yield return new WaitForSeconds(1f);
            updateDash();
        }

        if (dashCdRem <= 0)
        {
            dashbool = true;
            dashCdRem = dashCd;
            updateDash();
        }
    }

    IEnumerator moveorbs()
    {
        orbmoving = true;
        for (int i = 0; i < 4; i++)
        {
            float fracComplete = (Time.time - startTime) / journeyTime;
            centerPoint = (startPos[i].position + endPos[i].position) * 0.5f;
            centerPoint += Vector3.forward * 3f;
            startRelCenter = startPos[i].position - centerPoint;
            endRelCenter = endPos[i].position - centerPoint;
            Balls[i].transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete);
            Balls[i].transform.position += centerPoint;
        }
        yield return new WaitForSeconds(0.5f);

        if (startV == EndV)
        {
            orbmoving = false;
        }
        yield return null;
    }

    IEnumerator FreeDelay()
    {
        yield return new WaitForSeconds(FreeSpeed);
        allowfire = true;
    }

    IEnumerator WallDelay()
    {
        yield return new WaitForSeconds(WallSpeed);
        for (int i = 0; i < 4; i++)
        {
            chargeValue[i] = 0;
        }
        allowfire = true;
    }

    IEnumerator cooling()
    {
        coolingbool = true;
        yield return new WaitForSeconds(0.5f);
        int coolingamount = 0;
        if (startV == Free[1].transform.position)
        {
            coolingamount = 2;
        }
        else if (startV == Lazer[1].transform.position && beam.activeSelf == false && allowfire)
        {
            coolingamount = 1;
        }
        else
        {
            coolingamount = 0;
        }

        if (heating > 0 && allowfire)
        {
            heating -= coolingamount;
        }
        coolingbool = false;
    }

    IEnumerator heatingCoroutine()
    {

        heatingbool = true;
        int heatamount = 0;
        if (startV == Lazer[1].position)
        {
            Debug.Log("Heating laser");
            heatamount = 10;
            if (heating <= maxHeat && beam.activeSelf == true)
            {
                heating += heatamount;
            }
        }

        else if (startV == Wall[1].position)
        {
            heatamount = 2;
            if (heating <= maxHeat && !overheatbool)
            {
                heating += heatamount;
            }
        }

        else if (startV == Shield[1].position)
        {
            heatamount = 5;
            if (heating <= maxHeat - heatamount && !overheatbool)
            {
                heating += heatamount;
            }
        }

        yield return new WaitForSeconds(0.5f);
        heatingbool = false;
    }

    IEnumerator overheat()
    {
        allowfire = false;
        overheatbool = true;

        while(heating >= 50)
        {
            heating -= 5;
            yield return new WaitForSeconds(1f);
        }
        if(heating <= 50)
        {
            overheatbool = false;
            allowfire = true;
        }
    }
}
