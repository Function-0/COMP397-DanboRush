using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource clickSound;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.None;
    }

    //Method to create sound effect
    public void SoundEffect()
    {
        clickSound.Play();
    }

    //Method for the Restart button SceneManager.GetActiveScene().buildIndex + 1
    public void Restart()
    {
        SoundEffect();
        SceneManager.LoadScene("Prototype_1");
    }

    //Method for Main Menu
    public void MainMenu()
    {
        SoundEffect();
        SceneManager.LoadScene("MainMenu");
    }

    //Method For Quit
    public void QuitGame()
    {
        SoundEffect();
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }
}
