/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-16 18:04:35 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-16 18:49:13
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTutorial : Tutorial
{
    // Observer Pattern - Observer
    void OnEnable()
    {
        TutorialPlayerBehaviour.FireEvent += Done;
    }
    public override void CheckIfHappening()
    {
        if (isCompleted)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }
}
