﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageManagement : MonoBehaviour {

    GameObject player;
    float playerposx;
    GameObject lazertrigger;
    float lazertriggerposx;

    int score;
    public Text scoreText;

    bool gamepaused = false;
    bool playerdead;
    bool deathplayed = false;

    public GameObject pauseCanvas;

    public bool lazermove = false;
    public AudioSource deathsound;

	// Use this for initialization
	void Start () {
        score = 0;
        lazertrigger = GameObject.Find("Lazertrigger");
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        playerdead = player.GetComponent<playerMovement>().playerDied;
        playerposx = player.transform.position.x;
        lazertriggerposx = lazertrigger.transform.position.x;

        Debug.Log(score);

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
            PlayerPrefs.SetInt("CurScore", score);
            PlayerPrefs.Save();
            StartCoroutine(returnLab());
        }
	}

    public void addscore(int scorevalue)
    {
        score = score + scorevalue;
        updateScore();
    }

    void updateScore()
    {
        scoreText.text = score.ToString();
    }


    IEnumerator returnLab()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation async = SceneManager.LoadSceneAsync("labLevel", LoadSceneMode.Single);
        Time.timeScale = 1;
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
