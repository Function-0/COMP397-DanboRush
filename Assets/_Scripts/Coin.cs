using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float coins = 10f;
    public CoinScript coinScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(90 * Time.deltaTime, 0, 0);
    }

   void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            coinScript.TakeCoin(coins);
            Destroy(gameObject);
        }
    }
}
