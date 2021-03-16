using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    public Vector3 enterPos;
    public Vector3 exitPos;

    private float damage = 10f;
    private PlayerBehaviour playerBehaviour;

     void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            print("enter");

            enterPos = transform.position;

            if(exitPos.y - enterPos.y > 1.5)
            {
                print("falling damage");

                playerBehaviour.TakeDamage(damage);
            }
        }
    }

     void OnTriggerExit(Collider other)
    {
        if(other.tag == "ground")
        {
            print("exit");

            exitPos = transform.position;
        }
    }
}
