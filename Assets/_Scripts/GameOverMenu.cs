/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 14:06:42 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-07 14:26:25
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public List<MonoBehaviour> scripts;
    public AudioSource clickSound;
    public GameObject gameOverCanvas;
    public GameObject gameOverMenuUI;

    void Start() {
        gameOverCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: change the condition to time out or the player is dead
        if (Input.GetKeyDown(KeyCode.G)) {
            gameOverCanvas.GetComponent<Canvas>().enabled = true;
            GameOver();
        }
    }

    public void GameOver() {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

        foreach(var script in scripts) {
            script.enabled = false;
        }
    }

    public void Restart() {
        playClickSoundEffect();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype_1");
    }

    public void LoadMainMenu() {
        playClickSoundEffect();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void playClickSoundEffect() {
        clickSound.Play();
    } 
}
