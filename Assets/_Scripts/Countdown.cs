using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeStart = 60;
    public TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "Truck Leaves In: " + timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        textBox.text = "Truck Leaves In: " + Mathf.Round(timeStart).ToString();

        if (timeStart <= 0)
        {
            FindObjectOfType<GameController>().GameOver();
        }
    }

    public void AddTime(float time)
    {
        timeStart += time;
    }
}
