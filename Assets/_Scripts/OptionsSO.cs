/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-07 19:49:32
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-14 15:47:45
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Options", menuName = "OptionsMenu/Options")]
public class OptionsSO : ScriptableObject
{
    public float musicVolume;
    public float soundVolume;
    public KeyCode forwardKey;
    public KeyCode backwardKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode pauseKey;
    public KeyCode boostKey;
    public KeyCode inventoryKey;
    public KeyCode swapKey;
    public KeyCode miniMapKey;
}
