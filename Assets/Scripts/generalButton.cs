using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class generalButton : MonoBehaviour {

    public void reload()
    {
        string curScene = SceneManager.GetActiveScene().ToString();
        SceneManager.LoadScene(curScene);
    }
	
}
