using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ThrowBoxInventory : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private int quantity = 0;
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
        quantity += count;
        textMesh.text = quantity.ToString()
            ;
    }
}
