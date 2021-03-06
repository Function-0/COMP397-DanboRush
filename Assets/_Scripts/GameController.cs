/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 14:53:38 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-13 00:28:55
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
    public GameObject achievementsMenu;

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

    [Header("Enemy")]
    public EnemyTest enemy;

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
    public Counter envelope;
    public InventoryController inventoryController;

    private int messageTimer = 0;
    private int messageTimeOut = 400;

    // Start is called before the first frame update
    void Start()
    {
        ResetGameStateToDefault();
        gameTime = FindObjectOfType<Countdown>();
        gameScore = FindObjectOfType<ScoreController>();
        coin = FindObjectOfType<CoinScript>();
        envelope = FindObjectOfType<Counter>();
        inventoryController = FindObjectOfType<InventoryController>();
        enemy = FindObjectOfType<EnemyTest>();

        // Load the game from the MainMenu scene
        if (PlayerPrefs.HasKey("LoadGame"))
        {
            switch (PlayerPrefs.GetInt("LoadGame"))
            {
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
        Cursor.lockState = CursorLockMode.None;
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
        Cursor.lockState = CursorLockMode.None;

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

    public void Restart()
    {
        PlayClickSoundEffect();
        ResetGameStateToDefault();
        SceneManager.LoadScene("Prototype_1");
    }

    public void ToggleSaveGameMenu()
    {
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
        sceneData.envelope = envelope.score;
        sceneData.inventoryQuantity = inventoryController.inventoryList.Count;

        sceneData.enemyPosition = enemy.transform.position;
        sceneData.enemyRotation = enemy.transform.rotation;
        sceneData.enemyHealth = enemy.health;

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

    public void ToggleLoadGameMenu()
    {
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

        // enemy = FindObjectsOfType<Enemy>();
        //enemy = sceneData.enemyNumber;

        //foreach (Enemy e in enemy)
        //{
        //    e.transform.position = sceneData.enemyPosition;
        //    e.transform.rotation = sceneData.enemyRotation;
        //    e.health = sceneData.enemyHealth;
        //    Debug.Log("enemy's health: " + e.health);
        //    e.healthBar.SetHealthValue(sceneData.enemyHealth);
        //}

        enemy.transform.position = sceneData.enemyPosition;
        enemy.transform.rotation = sceneData.enemyRotation;
        enemy.health = sceneData.enemyHealth;
        Debug.Log("enemy's health: " + enemy.health);
        enemy.healthBar.SetHealthValue(sceneData.enemyHealth);

        gameTime.timeStart = sceneData.time;
        gameScore.score = sceneData.score;
        coin.currentCoin = sceneData.coin;
        coin.SetCoinBarValue(sceneData.coin);
        envelope.score = sceneData.envelope;
        envelope.SetEnvelopeValue(sceneData.envelope);
        inventoryController.LoadInventoryItems(sceneData.inventoryQuantity);

        if (isPaused)
        {
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

    public void ToggleOptionsMenu()
    {
        PlayClickSoundEffect();
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    public void ToggleAchievementsMenu()
    {
        PlayClickSoundEffect();
        achievementsMenu.SetActive(!achievementsMenu.activeInHierarchy);
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.lockState = CursorLockMode.None;
        ToggleScripts();
    }

    public void MainMenu()
    {
        PlayClickSoundEffect();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        PlayClickSoundEffect();
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOverMenu.SetActive(true);

        FindObjectOfType<AudioController>().StopSound(0);

        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        ToggleScripts();
    }

}


/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 14:53:38 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-21 06:23:33
 */
/*
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

    [Header("Enemy")]
    public Enemy[] enemy;

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
    public Counter envelope;

    private int messageTimer = 0;
    private int messageTimeOut = 400;

    // Start is called before the first frame update
    void Start()
    {
        ResetGameStateToDefault();
        gameTime = FindObjectOfType<Countdown>();
        gameScore = FindObjectOfType<ScoreController>();
        coin = FindObjectOfType<CoinScript>();
        envelope = FindObjectOfType<Counter>();
        enemy = FindObjectsOfType<Enemy>();

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
        Cursor.lockState = CursorLockMode.None;
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
        Cursor.lockState = CursorLockMode.None;
        
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
        sceneData.envelope = envelope.score;

        //enemy = FindObjectsOfType<Enemy>();
        //sceneData.enemyNumber = enemy;
        //sceneData.enemyPosition = enemy.transform.position;
        //sceneData.enemyRotation = enemy.transform.rotation;
        //sceneData.enemyHealth = enemy.health;

        foreach (Enemy e in enemy)
        {
            //sceneData.enemyNumber = enemy;
            sceneData.enemyPosition = e.transform.position;
            sceneData.enemyRotation = e.transform.rotation;
            sceneData.enemyHealth = e.health;
        }

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

        // enemy = FindObjectsOfType<Enemy>();
        //enemy = sceneData.enemyNumber;

        foreach (Enemy e in enemy)
        {
            e.transform.position = sceneData.enemyPosition;
            e.transform.rotation = sceneData.enemyRotation;
            e.health = sceneData.enemyHealth;
            Debug.Log("enemy's health: " + e.health);
            e.healthBar.SetHealthValue(sceneData.enemyHealth);
        }

        //enemy.transform.position = sceneData.enemyPosition;
        //enemy.transform.rotation = sceneData.enemyRotation;
        //enemy.health = sceneData.enemyHealth;
        //Debug.Log("enemy's health: " + enemy.health);
        //enemy.healthBar.SetHealthValue(sceneData.enemyHealth);

        gameTime.timeStart = sceneData.time;
        gameScore.score = sceneData.score;
        coin.currentCoin = sceneData.coin;
        coin.SetCoinBarValue(sceneData.coin);
        envelope.score = sceneData.envelope;
        envelope.SetEnvelopeValue(sceneData.envelope);
        
        
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
*/
