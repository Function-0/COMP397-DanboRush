/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 14:53:38 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-14 23:44:47
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject inventory;
    public GameObject miniMap;
    public GameObject optionsMenu;
    public GameObject gameOverMenu;
    public GameObject saveMenu;
    public GameObject loadMenu;

    [Header("Save/Load Slots")]
    public TextMeshProUGUI saveSlot1;
    public TextMeshProUGUI saveSlot2; 
    public TextMeshProUGUI saveSlot3;
    public TextMeshProUGUI loadSlot1;
    public TextMeshProUGUI loadSlot2;
    public TextMeshProUGUI loadSlot3;

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

    [Header("Game States")]
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

    // Load the keys and volumes from currentOptions
    public void LoadCurrentOptions()
    {
        optionsMenu.GetComponent<OptionsMenu>().SetMusicVolume(currentOptions.musicVolume);
        optionsMenu.GetComponent<OptionsMenu>().SetSoundVolume(currentOptions.soundVolume);
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
        saveMenu.SetActive(false);
        loadMenu.SetActive(false);
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

    public void ToggleSaveGameMenu() {
        PlayClickSoundEffect();
        PopulateSaveLoadMenu();
        saveMenu.SetActive(!saveMenu.activeInHierarchy);
    }

    private void PopulateSaveLoadMenu()
    {
        PlayerData slot1 = SaveSystem.LoadPlayer("Slot1");
        PlayerData slot2 = SaveSystem.LoadPlayer("Slot2");
        PlayerData slot3 = SaveSystem.LoadPlayer("Slot3");

        String saveTime;

        if (slot1 != null)
        {
            saveTime = slot1.saveTime.ToString();
            saveSlot1.text = saveTime;
            loadSlot1.text = saveTime;
        }

        if (slot2 != null)
        {
            saveTime = slot2.saveTime.ToString();
            saveSlot2.text = saveTime;
            loadSlot2.text = saveTime;
        }
        
        if (slot3 != null)
        {
            saveTime = slot3.saveTime.ToString();
            saveSlot3.text = saveTime;
            loadSlot3.text = saveTime;
        }
    }

    private void SaveGame(string slot)
    {
        PlayClickSoundEffect();
        DateTime saveTime = DateTime.Now;
        SaveSystem.SavePlayer(player, slot, saveTime);
        PopulateSaveLoadMenu();
    }

    public void SaveGameSlot1()
    {
        SaveGame("Slot1");
        Debug.Log("Save Slot1 Completed");
    }

    public void SaveGameSlot2()
    {
        SaveGame("Slot2");
        Debug.Log("Save Slot2 Completed");
    }

    public void SaveGameSlot3()
    {
        SaveGame("Slot3");
        Debug.Log("Save Slot3 Completed");
    }

    public void ToggleLoadGameMenu() {
        PlayClickSoundEffect();
        PopulateSaveLoadMenu();
        loadMenu.SetActive(!loadMenu.activeInHierarchy);
    }

    private void LoadGame(string slot)
    {
        PlayerData data = SaveSystem.LoadPlayer(slot);
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;
        Resume();
    }

    public void LoadGameSlot1()
    {
        PlayClickSoundEffect();
        LoadGame("Slot1");
        Debug.Log("Load Slot1 Completed");
    }

    public void LoadGameSlot2()
    {
        PlayClickSoundEffect();
        LoadGame("Slot2");
        Debug.Log("Load Slot2 Completed");
    }

    public void LoadGameSlot3()
    {
        PlayClickSoundEffect();
        LoadGame("Slot3");
        Debug.Log("Load Slot3 Completed");
    }
    
    public void ToggleOptionsMenu() {
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
