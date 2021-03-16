using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CoinScript : MonoBehaviour
{
    [Header("Coin Properties")]
    [Range(0, 100)]
    public float currentCoin = 100;
    [Range(1, 100)]
    public float maximumCoin = 100;

    public Slider coinBarSlider;

    // Start is called before the first frame update
    void Start()
    {
        coinBarSlider = GetComponent<Slider>();
        currentCoin = maximumCoin;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeCoin(float coin)
    {
        coinBarSlider.value += coin;
        currentCoin += coin;
        if (currentCoin < 0)
        {
            coinBarSlider.value = 0;
            currentCoin = 0;
        }
    }

    public void Reset()
    {
        coinBarSlider.value = maximumCoin;
        currentCoin = maximumCoin;
    }
}