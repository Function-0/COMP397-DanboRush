/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-07 16:48:00 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-02-15 00:21:38
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public AudioSource clickSound;
    public static bool isPaused = false;
    public GameObject pauseCanvas;
    public GameObject pauseMenuUI;

    void Start() {
        pauseCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseCanvas.GetComponent<Canvas>().enabled = true;
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        playClickSoundEffect();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        playClickSoundEffect();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart() {
        playClickSoundEffect();
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void Save() {
        playClickSoundEffect();
        Debug.Log("Save...");
    }

    public void Load() {
        playClickSoundEffect();
        Debug.Log("Load...");
    }

    public void LoadOptionsMenu() {
        playClickSoundEffect();
        Time.timeScale = 1f;
        SceneManager.LoadScene("OptionsMenu");
    }

    public void Quit() {
        playClickSoundEffect();
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }

    public void playClickSoundEffect() {
        clickSound.Play();
    } 
}
