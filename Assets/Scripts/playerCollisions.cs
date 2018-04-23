using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollisions : MonoBehaviour
{
    public GameObject deathCanvas;
    public bool playerDead = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "FollowLaser")
        {
            Debug.Log("OsuuCol");
            deathCanvas.SetActive(true);
            playerDead = true;
            StartCoroutine(playerDeath());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FollowLaser" ||  other.gameObject.tag == "Enemy" )
        {
            Debug.Log("OsuuTri");
            deathCanvas.SetActive(true);
            playerDead = true;
            StartCoroutine(playerDeath());
        }
    }

    IEnumerator playerDeath()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f);

    }
}
