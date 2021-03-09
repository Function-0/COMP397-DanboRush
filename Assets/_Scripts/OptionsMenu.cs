/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-05  ‏‎16:42:26
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-07 21:06:05
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("OptionsSO")]
    public OptionsSO currentOptions;
    public OptionsSO defaultOptions;
    public GameController gameController;
    public PlayerBehaviour player;

    [Header("Sound Settings")]
    public AudioMixer audioMixer;
    public AudioSource clickSound;
    
    [Header("Key Controls")]
    public Button[] buttons;
    public Text forward, backward, left, right, jump, pause, boost, inventory, swap, miniMap;
    public Toggle invertXAxis, invertYAxis;
    public Slider musicSlider, soundSlider;
    
    private Dictionary<string, KeyCode> keyMapping = new Dictionary<string, KeyCode>();
    private GameObject currentKey;
    public static bool invertXState, invertYState = false;

    // Use this for initialization
    void Start () {
        SetBtnSelectedColor();
        SetUpKeyMappingDict();
        UpdateKeyControlText();
    }

    // Update is called once per frame
    void Update () {

    }

    private void SetBtnSelectedColor() {
        Color32 selectedColor = new Color32(253, 161, 71, 255);

        foreach(Button btn in buttons) {
            ColorBlock cb = btn.colors;
            cb.selectedColor  = selectedColor;
            btn.colors = cb;
        }
    }

    // Update Key mapping on UI
    void OnGUI() {
        if (currentKey != null) {
            Event e = Event.current;
            if (e.isKey) {
                keyMapping[currentKey.name] = e.keyCode;

                string keyName = currentKey.name.Replace("Btn", "");
                Debug.Log(keyName);
                UpdateCurrentOptions(keyName, e.keyCode);

                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    public void UpdateCurrentOptions(string keyName, KeyCode key) {
        switch(keyName) {
            case "Forward":
                currentOptions.forwardKey = key;
                break;
            case "Backward":
                currentOptions.backwardKey = key;
                break;
            case "Left":
                currentOptions.leftKey = key;
                break;
            case "Right":
                currentOptions.rightKey = key;
                break;
            case "Jump":
                currentOptions.jumpKey = key;
                break;
            case "Pause":
                currentOptions.pauseKey = key;
                break;
            case "Boost":
                currentOptions.boostKey = key;
                break;
            case "Inventory":
                currentOptions.inventoryKey = key;
                break;
            case "Swap":
                currentOptions.swapKey = key;
                break;
            case "MiniMap":
                currentOptions.miniMapKey = key;
                break;
        }
        
        if (gameController) {
            gameController.LoadCurrentOptions();
        }

        if (player) {
            player.LoadCurrentOptions();
        }
    }

    // Click event listener for key mappings
    public void ChangeKey(GameObject clickedKey) {
        Debug.Log(clickedKey);
        PlayClickSoundEffect();
        currentKey = clickedKey;
    }

    // Click event listener for InvertXAxis
    public void ToggleInvertXAxis() {
        PlayClickSoundEffect();
        invertXState = invertXAxis.isOn;
    }

    // Click event listener for InvertYAxis
    public void ToggleInvertYAxis() {
        PlayClickSoundEffect();
        invertYState = invertYAxis.isOn;
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume(float volume) {
        audioMixer.SetFloat("SoundVolume", volume);
    }

    public void PlayClickSoundEffect() {
        clickSound.Play();
    } 

    private void SetUpKeyMappingDict() {
        // Add key mapping
        keyMapping.Add("Forward", currentOptions.forwardKey);
        keyMapping.Add("Backward", currentOptions.backwardKey);
        keyMapping.Add("Left", currentOptions.leftKey);
        keyMapping.Add("Right", currentOptions.rightKey);
        keyMapping.Add("Jump", currentOptions.jumpKey);
        keyMapping.Add("Pause", currentOptions.pauseKey);
        keyMapping.Add("Boost", currentOptions.boostKey);
        keyMapping.Add("Inventory", currentOptions.inventoryKey);
        keyMapping.Add("Swap", currentOptions.swapKey);
        keyMapping.Add("MiniMap", currentOptions.miniMapKey);
    }

    private void UpdateKeyControlText() {
        // Reflect text on UI controls
        forward.text = keyMapping["Forward"].ToString();
        backward.text = keyMapping["Backward"].ToString();
        left.text = keyMapping["Left"].ToString();
        right.text = keyMapping["Right"].ToString();
        jump.text = keyMapping["Jump"].ToString();
        pause.text = keyMapping["Pause"].ToString();
        boost.text = keyMapping["Boost"].ToString();
        inventory.text = keyMapping["Inventory"].ToString();
        swap.text = keyMapping["Swap"].ToString();
        miniMap.text = keyMapping["MiniMap"].ToString();
    }

    public void LoadCurrentOptions() {
        keyMapping["Forward"] = currentOptions.forwardKey;
        keyMapping["Backward"] = currentOptions.backwardKey;
        keyMapping["Left"] = currentOptions.leftKey;
        keyMapping["Right"] = currentOptions.rightKey;
        keyMapping["Jump"] = currentOptions.jumpKey;
        keyMapping["Pause"] = currentOptions.pauseKey;
        keyMapping["Boost"] = currentOptions.boostKey;
        keyMapping["Inventory"] = currentOptions.inventoryKey;
        keyMapping["Swap"] = currentOptions.swapKey;
        keyMapping["MiniMap"] = currentOptions.miniMapKey;

        UpdateKeyControlText();

        if (gameController) {
            gameController.LoadCurrentOptions();
        }

        if (player) {
            player.LoadCurrentOptions();
        }
    }

    public void ResetToDefaults()
    {
        PlayClickSoundEffect();
        // Reset Audio Settings
        musicSlider.value = 0;
        soundSlider.value = 0;

        // Reset Keyboard Controls Settings
        currentOptions.forwardKey = defaultOptions.forwardKey;
        currentOptions.backwardKey = defaultOptions.backwardKey;
        currentOptions.leftKey = defaultOptions.leftKey;
        currentOptions.rightKey = defaultOptions.rightKey;
        currentOptions.jumpKey = defaultOptions.jumpKey;
        currentOptions.pauseKey = defaultOptions.pauseKey;
        currentOptions.boostKey = defaultOptions.boostKey;
        currentOptions.inventoryKey = defaultOptions.inventoryKey;
        currentOptions.swapKey = defaultOptions.swapKey;
        currentOptions.miniMapKey = defaultOptions.miniMapKey;
        
        // Reset Mouse Controls Settings
        invertXState = false;
        invertYState = false;
        invertXAxis.isOn = invertXState;
        invertYAxis.isOn = invertYState;

        LoadCurrentOptions();
    }

}
