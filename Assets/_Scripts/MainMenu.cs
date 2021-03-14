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

    [Header("Menu")]
    public GameObject optionsMenu;

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
    public void OptionsMenu() {
        SoundEffect();
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    //Method For Quit
    public void QuitGame()
    {
        SoundEffect();
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }
}
