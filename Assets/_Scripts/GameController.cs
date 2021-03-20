/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 14:53:38 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-19 21:22:37
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;

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
    public SceneDataSO sceneDataSlot1;
    public SceneDataSO sceneDataSlot2;
    public SceneDataSO sceneDataSlot3;
    public GameObject saveMessage;

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

    [Header("Game Data")]
    public Countdown gameTime;
    public ScoreController gameScore;
    public CoinScript coin;

    private int messageTimer = 0;
    private int messageTimeOut = 400;

    // Start is called before the first frame update
    void Start()
    {
        ResetGameStateToDefault();
        gameTime = FindObjectOfType<Countdown>();
        gameScore = FindObjectOfType<ScoreController>();
        coin = FindObjectOfType<CoinScript>();

        // Load the game from the MainMenu scene
        if (PlayerPrefs.HasKey("LoadGame"))
        {
            switch (PlayerPrefs.GetInt("LoadGame")){
                case 1:
                    LoadGameSlot1();
                    break;
                case 2:
                    LoadGameSlot2();
                    break;
                case 3:
                    LoadGameSlot3();
                    break;
            }
        }
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

        if (saveMessage.activeInHierarchy)
        {
            HideSaveMessageAfterTimeOut();
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
        saveSlot1.text = loadSlot1.text = 
            String.IsNullOrEmpty(sceneDataSlot1.lastModifiedDate) ? "EMPTY" : sceneDataSlot1.lastModifiedDate;
        saveSlot2.text = loadSlot2.text = 
            String.IsNullOrEmpty(sceneDataSlot2.lastModifiedDate) ? "EMPTY" : sceneDataSlot2.lastModifiedDate;
        saveSlot3.text = loadSlot3.text = 
            String.IsNullOrEmpty(sceneDataSlot3.lastModifiedDate) ? "EMPTY" : sceneDataSlot3.lastModifiedDate;
    }

    private void ShowSaveMessage()
    {
        saveMessage.SetActive(true);
    }

    private void HideSaveMessageAfterTimeOut()
    {
        messageTimer++;
        if (messageTimer > messageTimeOut)
        {
            saveMessage.SetActive(false);
            messageTimer = 0;
        }
    }

    private void SaveGame(SceneDataSO sceneData)
    {
        sceneData.playerPosition = player.transform.position;
        sceneData.playerRotation = player.transform.rotation;
        sceneData.playerHealth = player.health;
        sceneData.lastModifiedDate = DateTime.Now.ToString();

        sceneData.time = Mathf.Round(gameTime.timeStart);
        sceneData.score = gameScore.score;
        sceneData.coin = coin.currentCoin;

        ShowSaveMessage();
        PopulateSaveLoadMenu();
    }

    public void SaveGameSlot1()
    {
        PlayClickSoundEffect();
        SaveGame(sceneDataSlot1);
        Debug.Log("Save Slot1 Completed");
    }

    public void SaveGameSlot2()
    {
        PlayClickSoundEffect();
        SaveGame(sceneDataSlot2);
        Debug.Log("Save Slot2 Completed");
    }

    public void SaveGameSlot3()
    {
        PlayClickSoundEffect();
        SaveGame(sceneDataSlot3);
        Debug.Log("Save Slot3 Completed");
    }

    public void ToggleLoadGameMenu() {
        PlayClickSoundEffect();
        PopulateSaveLoadMenu();
        loadMenu.SetActive(!loadMenu.activeInHierarchy);
    }

    private void LoadGame(SceneDataSO sceneData)
    {
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.rotation = sceneData.playerRotation;
        player.controller.enabled = true;
        player.health = sceneData.playerHealth;
        Debug.Log("Player's health: " + player.health);
        player.healthBar.SetHealthValue(sceneData.playerHealth);
        gameTime.timeStart = sceneData.time;
        gameScore.score = sceneData.score;
        coin.currentCoin = sceneData.coin;
        coin.SetCoinBarValue(sceneData.coin);
        
        if (isPaused) {
            Resume();
        }
    }

    public void LoadGameSlot1()
    {
        PlayClickSoundEffect();
        LoadGame(sceneDataSlot1);
        Debug.Log("Load Slot1 Completed");
    }

    public void LoadGameSlot2()
    {
        PlayClickSoundEffect();
        LoadGame(sceneDataSlot2);
        Debug.Log("Load Slot2 Completed");
    }

    public void LoadGameSlot3()
    {
        PlayClickSoundEffect();
        LoadGame(sceneDataSlot3);
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
