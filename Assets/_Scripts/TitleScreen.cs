using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class TitleScreen : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource clickSound;

    //Method to create sound effect
    public void SoundEffect()
    {
        clickSound.Play();
    }

    public void LoadMainMenu()
    {
        SoundEffect();
        SceneManager.LoadScene("MainMenu");
    }
}
