/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-03-19 14:50:01 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-04 21:41:51
 */
 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
public class SceneDataSO : ScriptableObject
{
    [Header("Player Data")]
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float playerHealth;

    [Header("Game Data")]
    public float time;
    public float score;
    public float coin;
    public int envelope;
    public int inventoryQuantity;
    public string lastModifiedDate;

    [Header("Enemy Data")]
    public Enemy[] enemyNumber;
    public Vector3 enemyPosition;
    public Quaternion enemyRotation;
    public float enemyHealth;
}

