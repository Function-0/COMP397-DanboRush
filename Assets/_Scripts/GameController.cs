/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 14:53:38 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-07 16:11:45
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject inventory;
    public GameObject miniMap;
    public GameObject optionsMenu;
    public GameObject gameOverMenu;

    [Header("Sound")]
    public AudioSource clickSound;

    [Header("Scripts to disable")]
    public List<MonoBehaviour> scripts;

    [Header("Player")]
    public PlayerBehaviour player;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            // toggle the Inventory on/off
            inventory.SetActive(!inventory.activeInHierarchy);
            playClickSoundEffect();
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            // toggle the MiniMap on/off
            miniMap.SetActive(!miniMap.activeInHierarchy);
            playClickSoundEffect();
        }

        // TODO: change the condition to time out or the player is dead
        else if (Input.GetKeyDown(KeyCode.G)) {
            gameOverMenu.SetActive(true);
            GameOver();
        }
    }

    public void playClickSoundEffect()
    {
        clickSound.Play();
    }

    public void Resume()
    {
        playClickSoundEffect();
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        toggleScripts();
    }

    public void Pause()
    {
        playClickSoundEffect();
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
        
        toggleScripts();
    }
    
    private void toggleScripts()
    {
        foreach (var script in scripts)
        {
            script.enabled = !isPaused;
        }
    }

    public void Restart() {
        isPaused = false;
        playClickSoundEffect();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype_1");
    }

    public void SaveGame() {
        playClickSoundEffect();
        Debug.Log("Save...");
        SaveSystem.SavePlayer(player);
    }

    public void LoadGame() {
        playClickSoundEffect();
        Debug.Log("Load...");
        
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;
        
        Resume();
    }
    
    public void OptionsMenu() {
        playClickSoundEffect();
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    public void MainMenu() {
        playClickSoundEffect();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        playClickSoundEffect();
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }

    public void GameOver() {
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        toggleScripts();
    }
}
