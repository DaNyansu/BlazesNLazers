using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour {

    public Text currentscoretext;
    public Text highscoretext;

    int highscore;
    int currentscore;

    // Use this for initialization
    void Start () {
        currentscore = PlayerPrefs.GetInt("CurScore");
        currentscoretext.text = "Current Score: " + currentscore.ToString();

        highscore = PlayerPrefs.GetInt("HighScore");
        checkHS();

        highscoretext.text = "High Score: " + highscore.ToString();
		
	}
	
    public void checkHS()
    {
        if(currentscore > highscore)
        {
            highscore = currentscore;
            PlayerPrefs.SetInt("HighScore", highscore);
            PlayerPrefs.Save();
        }
    }
}
