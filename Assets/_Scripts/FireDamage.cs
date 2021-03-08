using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireDamage : MonoBehaviour
{
    public float lookRadius = 5f;

	private float damage = 10f;

    private GameObject player;
	Transform target;

	private PlayerBehaviour playerBehaviour;


	// Start is called before the first frame update
	void Start()
    {
        player = GameObject.Find("Amazon danbo");
        target = player.transform;
		playerBehaviour = FindObjectOfType<PlayerBehaviour>();
	}

	// Update is called once per frame
	void Update()
	{
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

       // If inside the radius

        if (distance <= lookRadius)
        {
            playerBehaviour.TakeDamage(damage);
        }

    }
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
