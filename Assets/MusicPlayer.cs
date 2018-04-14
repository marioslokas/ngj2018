using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public AudioSource slowMusic;

    public AudioSource fastMusic;

    public bool playSlow;

    public bool playFast;

	// Use this for initialization
	void Start () {
        slowMusic.volume = 0.3f;
        fastMusic.volume = 0.0f;
        slowMusic.Play();
        fastMusic.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (playFast)
        {
            TransitionToFast();

        } else if (playSlow)
        {
            TransitionToSlow();
        }
	}


    void TransitionToSlow()
    {
        fastMusic.volume = Mathf.Lerp(0.8f, 0.0f, -Time.time);
    }

    void TransitionToFast()
    {
        fastMusic.volume = Mathf.Lerp(0.0f, 0.8f, Time.time);
    }
}
