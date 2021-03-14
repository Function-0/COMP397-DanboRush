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

    public static bool IsGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetGameStateToDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGameOver)
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
        }
    }

    // Load the keys from currentOptions
    public void LoadCurrentOptions()
    {
        pauseKey = currentOptions.pauseKey;
        inventoryKey = currentOptions.inventoryKey;
        miniMapKey = currentOptions.miniMapKey;
    }

    private void ResetGameStateToDefault()
    {
        isPaused = false;
        IsGameOver = false;
        Time.timeScale = 1f;
        LoadCurrentOptions();
        ToggleScripts();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayClickSoundEffect()
    {
        clickSound.Play();
    }

    public void Resume()
    {
        PlayClickSoundEffect();
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        optionsMenu.SetActive(false); // close the Options menu as well
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
        PlayClickSoundEffect();
        ResetGameStateToDefault();
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
        IsGameOver = true;
        gameOverMenu.SetActive(true);

        FindObjectOfType<AudioController>().StopSound(0);

        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        ToggleScripts();
    }

}
