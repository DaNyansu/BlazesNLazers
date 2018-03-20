using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollisions : MonoBehaviour {
    public GameObject deathCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "FollowLazer")
        {
            Debug.Log("Osuu");
            deathCanvas.SetActive(true);
            StartCoroutine(playerDeath());
        }
    }

    IEnumerator playerDeath()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(2f);
      
    }
}
