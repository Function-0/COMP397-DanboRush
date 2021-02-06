/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-05  ‏‎16:42:26
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-02-05 17:49:28
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private Dictionary<string, KeyCode> keyMapping = new Dictionary<string, KeyCode>();
    public Text forward, backward, left, right, jump, pause, boost, inventory, swap;
    private GameObject currentKey;

    public Button[] buttons;

    // Use this for initialization
    void Start () {
        setBtnSelectedColor();

        Debug.Log(buttons.Length);
        // Add key mapping
        keyMapping.Add("Forward", KeyCode.W);
        keyMapping.Add("Backward", KeyCode.S);
        keyMapping.Add("Left", KeyCode.A);
        keyMapping.Add("Right", KeyCode.D);
        keyMapping.Add("Jump", KeyCode.Space);
        keyMapping.Add("Pause", KeyCode.Escape);
        keyMapping.Add("Boost", KeyCode.LeftShift);
        keyMapping.Add("Inventory", KeyCode.E);
        keyMapping.Add("Swap", KeyCode.Q);

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
    }

    // Update is called once per frame
    void Update () {

    }

    private void setBtnSelectedColor() {
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
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    // Click event listener
    public void ChangeKey(GameObject clickedKey) {
        currentKey = clickedKey;
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume(float volume) {
        audioMixer.SetFloat("SoundVolume", volume);
    }

}
