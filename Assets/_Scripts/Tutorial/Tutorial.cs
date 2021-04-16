/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-16 16:32:44 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-16 18:51:57
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int Order;

    [TextArea(3, 10)]
    public string Instruction;
    protected bool isCompleted = false;
    protected void Done() {
        isCompleted = true;
    }

    void Awake()
    {
        TutorialManager.Instance.Tutorials.Add(this);
    }

    public virtual void CheckIfHappening() { }

}
