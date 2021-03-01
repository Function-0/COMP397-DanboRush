/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-28 17:46:49 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-02-28 19:34:18
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position; // Vector3 can't be serialized, use float[] instead
    
    public PlayerData (PlayerBehaviour player) {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        Debug.Log("PlayerData Position: (" + position[0] + ", " + position[1] + ", " + position[2] + ")");
    }

}
