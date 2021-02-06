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

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume(float volume) {
        audioMixer.SetFloat("SoundVolume", volume);
    }

}
