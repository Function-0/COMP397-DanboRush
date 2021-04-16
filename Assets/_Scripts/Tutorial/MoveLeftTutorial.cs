/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-16 16:48:16 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-16 17:37:08
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftTutorial : Tutorial
{
    public Joystick joystick;

    public override void CheckIfHappening()
    {
        float x = joystick.Horizontal;
        if (x < 0)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
        // float z = joystick.Vertical;

        // Debug.Log(z);

        // for (int i = 0; i < directions.Count; i++)
        // {
        //     if (x < 0)
        //     {
        //         directions.Remove("Left");
        //         break;
        //     }

        //     if (x > 0)
        //     {
        //         directions.Remove("Right");
        //         break;
        //     }

        //     if (z < 0)
        //     {
        //         directions.Remove("Backward");
        //         break;
        //     }

        //     if (z > 0)
        //     {
        //         directions.Remove("Forward");
        //         break;
        //     }
        // }

        // if (directions.Count == 0)
        // {
        //     TutorialManager.Instance.CompletedTutorial();
        // }
    }

}
