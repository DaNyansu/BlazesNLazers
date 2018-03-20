using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageManagement : MonoBehaviour {

    GameObject player;
    float playerposx;
    GameObject lazertrigger;
    float lazertriggerposx;

    bool allowfire;
    bool gamepaused = false;

    public GameObject pauseCanvas;

    public bool lazermove = false;

	// Use this for initialization
	void Start () {
        lazertrigger = GameObject.Find("Lazertrigger");
        player = GameObject.Find("Player");
        allowfire = player.GetComponent<playerMovement>().allowfire;
    }
	
	// Update is called once per frame
	void Update () {
        playerposx = player.transform.position.x;
        lazertriggerposx = lazertrigger.transform.position.x;
        

        if (playerposx >= lazertriggerposx)
        {
            Debug.Log("triggered");
            lazermove = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Peli on pausella
            gamepaused = true;
            Time.timeScale = 0;
        }

        if(Time.timeScale == 0 && gamepaused)
        {
            pauseCanvas.SetActive(true);
        }

        else
        {
            pauseCanvas.SetActive(false);
        }
	}
}
