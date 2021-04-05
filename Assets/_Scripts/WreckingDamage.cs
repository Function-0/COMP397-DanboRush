using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingDamage : MonoBehaviour
{

	public float lookRadius = 5f;

	public AudioClip audioClip;
	public float volume;
	AudioSource wreckingSound;
	public bool alreadyPlayed = false;

	private float damage = 10f;

	public float fireRate = 1f;
	private float nextTimeToFire = 0f;

	private GameObject player;
	Transform target;

	private PlayerBehaviour playerBehaviour;


	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Amazon danbo");
		target = player.transform;
		playerBehaviour = FindObjectOfType<PlayerBehaviour>();
		wreckingSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{


		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the radius

		if (distance <= lookRadius && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			playerBehaviour.TakeDamage(damage);
		}

	}

	void OnTriggerEnter()
	{
		if (!alreadyPlayed)
		{
			wreckingSound.PlayOneShot(audioClip, volume);
		//	alreadyPlayed = true;
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}