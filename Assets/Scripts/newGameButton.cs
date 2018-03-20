using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newGameButton : MonoBehaviour {
    bool loadLevel = false;
    public GameObject loading;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void continueB()
    {
        if (!loadLevel)
        {
            loadLevel = true;
            StartCoroutine(LoadingLevel());
        }

        if (loadLevel)
        {
            loading.SetActive(true);
        }

    }

    IEnumerator LoadingLevel()
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation async = SceneManager.LoadSceneAsync("Prototype", LoadSceneMode.Single);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
