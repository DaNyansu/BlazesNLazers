using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelEffect : MonoBehaviour {

    Color panelcolor;
    Image panelimg;

	// Use this for initialization
	void Start () {
        panelimg = GetComponent<Image>();
        panelcolor = panelimg.color;
    }
	
	// Update is called once per frame
	void Update () {
        if(gameObject.activeSelf == true)
        {
            panelimg.color = new Color(panelcolor.a, panelcolor.a,Mathf.PingPong(Time.time, 1));
        }
	}
}
