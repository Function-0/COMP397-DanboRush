using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CoinScript : MonoBehaviour
{
    [Header("Coin Properties")]
    [Range(0, 100)]
    public float currentCoin = 0;
    [Range(1, 100)]
    public float maximumCoin = 100;

    public Slider coinBarSlider;

    [Header("Sound")]
    public AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        coinBarSlider = GetComponent<Slider>();
        currentCoin = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeCoin(float coin)
    {   
        coinSound.Play();
        coinBarSlider.value += coin;
        currentCoin += coin;
        if (currentCoin > maximumCoin)
        {
            currentCoin = maximumCoin;
        }
    }

    public void Reset()
    {
        coinBarSlider.value = 0;
        currentCoin = 0;
    }

    public void SetCoinBarValue(float coin)
    {
        coinBarSlider.value = coin;
    }
}