using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> AudioClips = new List<AudioClip>();
    private AudioSource AudioSource;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int clip)
    {
        if (gameObject.activeSelf)
            AudioSource.PlayOneShot(AudioClips[clip]);
    }
}
