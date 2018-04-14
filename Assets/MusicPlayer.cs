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
            StartCoroutine(TransitionToFast());

        } else if (playSlow)
        {
            StartCoroutine(TransitionToSlow());
        }
	}

    IEnumerator TransitionToSlow()
    {
        float t = 0f;

        while((t < 3f) && (fastMusic.volume > 0.0f))
        {
            t += Time.deltaTime;

            fastMusic.volume -= 1.0f;

            yield return null;
        }
    }

    IEnumerator TransitionToFast()
    {
        float t = 0f;

        while ((t < 3f) && (fastMusic.volume < 1.0f))
        {
            t += Time.deltaTime;

            fastMusic.volume += 1.0f;

            yield return null;
        }

    }
}
