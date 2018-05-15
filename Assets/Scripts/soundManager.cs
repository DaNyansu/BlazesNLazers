using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {

    public AudioClip startClip;
    public AudioClip loopClip;

    AudioSource clip;

	// Use this for initialization
	void Start () {
        clip = GetComponent<AudioSource>();
        GetComponent<AudioSource>().loop = true;
        StartCoroutine(playLoopedClip());
        

    }
	
    IEnumerator playLoopedClip()
    {
        clip.clip = startClip;
        clip.Play();
        yield return new WaitForSeconds(startClip.length);
        clip.clip = loopClip;
        clip.Play();
     
    }
}
