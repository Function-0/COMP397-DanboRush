using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void PlaySound(int index)
    {
        AudioSource s = sounds[index];
        s.Play();
    }

    public void StopSound(int index)
    {
        AudioSource s = sounds[index];
        s.Stop();
    }
}