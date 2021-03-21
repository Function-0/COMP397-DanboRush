using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
       // textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void counter(int count)
    {
        score += count;
        textMesh.text = score.ToString() + "/5";
    }

    public void SetEnvelopeValue(int count)
    {
        textMesh.text = count.ToString() + "/5";
    }
}
