using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resumeButton : MonoBehaviour {
    public bool paused = true;

    bool loadLevel = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resumegame()
    {
        Time.timeScale = 1;
        paused = false;
    }

    public void mainmenubutton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Mainmenu",LoadSceneMode.Single);
    }

    public void reloadCurrent()
    {
        if (!loadLevel)
        {
            loadLevel = true;
            StartCoroutine(reloadlevel());
        }
    }

    IEnumerator reloadlevel()
    {
        Time.timeScale = 1;
        int scene = SceneManager.GetActiveScene().buildIndex;

        AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
