using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
	public float lookRadius = 4f;
	public AudioSource spikeSound;

	private float damage = 5f;

	private GameObject player;
	Transform target;

	private PlayerBehaviour playerBehaviour;


	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Amazon danbo");
		target = player.transform;
		playerBehaviour = FindObjectOfType<PlayerBehaviour>();
		spikeSound = GetComponent<AudioSource>();
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
		//if (distance <= soundRadius)
		//{
		//	spikeSound.Play();
		//}
		//else
		//{
		//	spikeSound.Stop();
		//}

	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
