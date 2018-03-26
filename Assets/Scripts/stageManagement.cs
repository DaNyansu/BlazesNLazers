using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageManagement : MonoBehaviour {

    GameObject player;
    float playerposx;
    GameObject lazertrigger;
    float lazertriggerposx;

    bool gamepaused = false;
    bool playerdead;
    bool deathplayed = false;

    public GameObject pauseCanvas;

    public bool lazermove = false;
    public AudioSource deathsound;

	// Use this for initialization
	void Start () {
        lazertrigger = GameObject.Find("Lazertrigger");
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        playerdead = player.GetComponent<playerMovement>().playerDied;
        playerposx = player.transform.position.x;
        lazertriggerposx = lazertrigger.transform.position.x;

        Debug.Log("äänet" + deathsound.isPlaying);

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

        if(playerdead && !deathplayed)
        {
            deathsound.Play();
            deathplayed = true;
        }
	}
}
