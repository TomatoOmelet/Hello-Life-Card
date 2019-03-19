using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource a;
    void Start()
    {
        a = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        a.clip = clip;
        a.Play();
    }
}
