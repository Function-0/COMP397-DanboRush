using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource clickSound;

    [Header("Menu")]
    public GameObject optionsMenu;
    public GameObject loadMenu;

    [Header("Load Slots")]
    public TextMeshProUGUI loadSlot1;
    public TextMeshProUGUI loadSlot2;
    public TextMeshProUGUI loadSlot3;
    public SceneDataSO sceneDataSlot1;
    public SceneDataSO sceneDataSlot2;
    public SceneDataSO sceneDataSlot3;

    //Method to create sound effect
    public void SoundEffect()
    {
        clickSound.Play();
    }

    //Method for the new game button SceneManager.GetActiveScene().buildIndex + 1
    public void NewGame()
    {
        SoundEffect();
        PlayerPrefs.DeleteKey("LoadGame");
        SceneManager.LoadScene("Prototype_1");
    }

    //Method for options
    public void ToggleOptionsMenu() {
        SoundEffect();
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    public void ToggleLoadMenu() {
        SoundEffect();
        PopulateLoadMenu();
        loadMenu.SetActive(!loadMenu.activeInHierarchy);
    }

    //Method For Quit
    public void QuitGame()
    {
        SoundEffect();
        SceneManager.LoadScene("ExitScreen");
        Application.Quit();
    }

    private void PopulateLoadMenu()
    {
        loadSlot1.text = 
            String.IsNullOrEmpty(sceneDataSlot1.lastModifiedDate) ? "EMPTY" : sceneDataSlot1.lastModifiedDate;
        loadSlot2.text = 
            String.IsNullOrEmpty(sceneDataSlot2.lastModifiedDate) ? "EMPTY" : sceneDataSlot2.lastModifiedDate;
        loadSlot3.text = 
            String.IsNullOrEmpty(sceneDataSlot3.lastModifiedDate) ? "EMPTY" : sceneDataSlot3.lastModifiedDate;
    }

    private void LoadGame(int slotNum)
    {
        SoundEffect();
        PlayerPrefs.SetInt("LoadGame", slotNum);
        SceneManager.LoadScene("Prototype_1");
    }

    public void OnLoadGameSlot1Clicked()
    {
        LoadGame(1);
    }

    public void OnLoadGameSlot2Clicked()
    {
        LoadGame(2);
    }

    public void OnLoadGameSlot3Clicked()
    {
        LoadGame(3);
    }

    public void Tutorial()
    {
        SoundEffect();
        PlayerPrefs.DeleteKey("LoadGame");
        SceneManager.LoadScene("Tutorial");
    }
}
