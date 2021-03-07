/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-07 16:48:00 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-07 15:44:59
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerBehaviour player;
    public List<MonoBehaviour> scripts;
    public AudioSource clickSound;
    public static bool isPaused = false;
    public GameObject pauseCanvas;
    public GameObject pauseMenuUI;

    void Start() {
        // pauseCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape)) {
        //     // pauseCanvas.GetComponent<Canvas>().enabled = true;
        //     if (isPaused) {
        //         Resume();
        //     }
        //     else {
        //         Pause();
        //     }
        // }
    }

    // public void Resume() {
    //     playClickSoundEffect();
    //     pauseMenuUI.SetActive(false);
    //     Time.timeScale = 1f;
    //     isPaused = false;
    //     Cursor.lockState = CursorLockMode.Locked;

    //     foreach(var script in scripts) {
    //         script.enabled = !isPaused;
    //     }
    // }

    // void Pause() {
    //     playClickSoundEffect();
    //     pauseMenuUI.SetActive(true);
    //     Cursor.lockState = CursorLockMode.None;
    //     Time.timeScale = 0f;
    //     isPaused = true;

    //     foreach(var script in scripts) {
    //         script.enabled = !isPaused;
    //     }
    // }

    // public void Restart() {
    //     playClickSoundEffect();
    //     Time.timeScale = 1f;
    //     SceneManager.LoadScene("Prototype_1");
    // }

    // public void Save() {
    //     playClickSoundEffect();
    //     Debug.Log("Save...");
    //     SaveSystem.SavePlayer(player);
    // }

    // public void Load() {
    //     playClickSoundEffect();
    //     Debug.Log("Load...");
        
    //     PlayerData data = SaveSystem.LoadPlayer();
    //     Vector3 position;
    //     position.x = data.position[0];
    //     position.y = data.position[1];
    //     position.z = data.position[2];
    //     player.transform.position = position;
        
    //     // Resume();
    // }

    // public void LoadOptionsMenu() {
    //     playClickSoundEffect();
    //     Time.timeScale = 1f;
    //     SceneManager.LoadScene("OptionsMenu");
    // }

    // public void Quit() {
    //     playClickSoundEffect();
    //     SceneManager.LoadScene("ExitScreen");
    //     Application.Quit();
    // }

    // public void playClickSoundEffect() {
    //     clickSound.Play();
    // } 
}
