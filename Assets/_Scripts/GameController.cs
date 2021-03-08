/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 14:53:38 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-07 20:51:01
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

    [Header("Input Options")]
    public OptionsSO currentOptions;
    public KeyCode pauseKey;
    public KeyCode inventoryKey;
    public KeyCode miniMapKey;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadCurrentOptions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
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

        else if (Input.GetKeyDown(inventoryKey))
        {
            // toggle the Inventory on/off
            inventory.SetActive(!inventory.activeInHierarchy);
            PlayClickSoundEffect();
        }

        else if (Input.GetKeyDown(miniMapKey))
        {
            // toggle the MiniMap on/off
            miniMap.SetActive(!miniMap.activeInHierarchy);
            PlayClickSoundEffect();
        }

        // TODO: change the condition to time out or the player is dead
        else if (Input.GetKeyDown(KeyCode.G)) {
            gameOverMenu.SetActive(true);
            GameOver();
        }
    }

    // Load the keys from currentOptions
    public void LoadCurrentOptions()
    {
        pauseKey = currentOptions.pauseKey;
        inventoryKey = currentOptions.inventoryKey;
        miniMapKey = currentOptions.miniMapKey;
    }

    public void PlayClickSoundEffect()
    {
        clickSound.Play();
    }

    public void Resume()
    {
        PlayClickSoundEffect();
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        ToggleScripts();
    }

    public void Pause()
    {
        PlayClickSoundEffect();
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
        
        ToggleScripts();
    }
    
    private void ToggleScripts()
    {
        foreach (var script in scripts)
        {
            script.enabled = !isPaused;
        }
    }

    public void Restart() {
        isPaused = false;
        PlayClickSoundEffect();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype_1");
    }

    public void SaveGame() {
        PlayClickSoundEffect();
        Debug.Log("Save...");
        SaveSystem.SavePlayer(player);
    }

    public void LoadGame() {
        PlayClickSoundEffect();
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
        PlayClickSoundEffect();
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    public void MainMenu() {
        PlayClickSoundEffect();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        PlayClickSoundEffect();
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }

    public void GameOver() {
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        ToggleScripts();
    }

}
