using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = Mathf.Round(FindObjectOfType<DistanceBarScreenSpaceController>().distanceLeft / 30).ToString();
    }
}
