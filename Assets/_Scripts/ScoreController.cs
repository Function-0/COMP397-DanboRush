using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public float score = 0;
    public TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = "Score: " + Mathf.Round(score).ToString();
    }

    public void AddScore(float score)
    {
        this.score += score;
    }
}
