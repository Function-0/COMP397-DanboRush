/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-16 17:38:45 
 * @Last Modified by:   Tzu-Ting Wu 
 * @Last Modified time: 2021-04-16 17:38:45 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightTutorial : Tutorial
{
    public Joystick joystick;

    public override void CheckIfHappening()
    {
        float x = joystick.Horizontal;
        if (x > 0)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }

}
