using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource clickSound;

    //Method to create sound effect
    public void SoundEffect()
    {
        clickSound.Play();
    }

    //Method for the new game button SceneManager.GetActiveScene().buildIndex + 1
    public void NewGame()
    {
        SoundEffect();
        SceneManager.LoadScene("Prototype_1");
    }

    //Method for options
    public void Options()
    {
        SoundEffect();
        SceneManager.LoadScene("OptionsMenu");
    }

    //Method For Quit
    public void QuitGame()
    {
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }
}
