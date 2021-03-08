using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireDamage : MonoBehaviour
{
    public GameObject Fire;
    public float lookRadius = 10f;

    public GameObject player;
	Transform target;


	// Start is called before the first frame update
	void Start()
    {
        player = GameObject.Find("Amazon danbo");
        target = player.transform;
	}

	// Update is called once per frame
	void Update()
	{
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the radius
		if (distance >= lookRadius)
		{
			Fire.SetActive(!Fire.activeInHierarchy);
        }
        else
        {
            Fire.SetActive(Fire.activeInHierarchy);
        }
    }
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
