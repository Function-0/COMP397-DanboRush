using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankControllerFinal : MonoBehaviour
{
    public float finalScore;
    public string rank;
    public TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        finalScore = PlayerPrefs.GetFloat("score");
        if (finalScore < 500)
        {
            textBox.text = "F";
        } 
        else if (finalScore < 1000)
        {
            textBox.text = "D";
        }
        else if (finalScore < 2000)
        {
            textBox.text = "C";
        }
        else if (finalScore < 3000)
        {
            textBox.text = "B";
        }
        else if (finalScore >= 4000)
        {
            textBox.text = "A";
        }

    }
}
