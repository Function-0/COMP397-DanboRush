/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-16 17:46:30 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-16 17:47:03
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackwardTutorial : Tutorial
{
    public Joystick joystick;

    public override void CheckIfHappening()
    {
        float z = joystick.Vertical;
        if (z < 0)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }

}
