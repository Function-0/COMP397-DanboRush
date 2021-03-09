using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreController : MonoBehaviour
{
    public float finalScore;
    public TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        finalScore = PlayerPrefs.GetFloat("score");
        textBox.text = finalScore.ToString();
    }
}
