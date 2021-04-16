/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-04-16 16:32:50 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-04-16 19:00:04
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> Tutorials = new List<Tutorial>();
    public GameObject instructionText;
    public GameObject startBtn;

    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();

            if (instance == null)
                Debug.Log("There is no TutorialManager");
            
            return instance;
        }
    }

    private Tutorial currentTutorial;

    // Start is called before the first frame update
    void Start()
    {
        SetNextTutorial(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }

    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.Order + 1);
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }

        instructionText.GetComponent<TextMeshProUGUI>().text = currentTutorial.Instruction;
    }

    public void CompletedAllTutorials()
    {
        instructionText.GetComponent<TextMeshProUGUI>().text = "Congratulation! You completed all the tutorials!!";

        startBtn.SetActive(true);
    }

    public Tutorial GetTutorialByOrder(int order) {
        {
            for (int i = 0; i < Tutorials.Count; i++)
            {
                if (Tutorials[i].Order == order)
                    return Tutorials[i];
            }
        }

        return null;
    } 

    public void OnStartBtnClicked()
    {
        SceneManager.LoadScene("Prototype_1");
    }
}
