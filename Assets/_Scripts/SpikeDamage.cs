using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
	public float lookRadius = 4f;

	public AudioClip audioClip;
	public float volume;
	AudioSource spikeSound;
	public bool alreadyPlayed = false;

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
    }
	void OnTriggerEnter()
	{
		if (!alreadyPlayed)
		{
			spikeSound.PlayOneShot(audioClip, volume);
			alreadyPlayed = true;
		}
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
