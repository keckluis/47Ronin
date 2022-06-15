using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip Bow;
    private AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }
}
