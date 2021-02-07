/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-07 16:48:00 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-02-07 17:26:44
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void Save() {
        Debug.Log("Save...");
    }

    public void Load() {
        Debug.Log("Load...");
    }

    public void LoadOptionsMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("OptionsMenu");
    }

    public void Quit() {
        Debug.Log("Quit....");
        Application.Quit();
    }
}
