using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBox : MonoBehaviour
{
    private int count = 1;
    public ThrowBoxInventory throwBoxInventory;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( 0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            throwBoxInventory.counter(count);
            Destroy(gameObject);
        }
    }
}
