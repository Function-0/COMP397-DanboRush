/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-16 17:50:15 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-16 18:50:35
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTutorial : Tutorial
{
    // Observer Pattern - Observer
    void OnEnable()
    {
        TutorialPlayerBehaviour.JumpEvent += Done;
    }

    public override void CheckIfHappening()
    {
        if (isCompleted)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }


    // private bool isCurrentTutorial = false;
    // public Transform hit;

    // public override void CheckIfHappening()
    // {
    //     isCurrentTutorial = true;
    // }

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (!isCurrentTutorial)
    //     {
    //         return;
    //     }

    //     if (other.transform == hit && hit.position.y > 3)
    //     {
    //         TutorialManager.Instance.CompletedTutorial();
    //         isCurrentTutorial = false;
    //     }
    // }
}
