using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireDamage : MonoBehaviour
{
    public float lookRadius = 5f;

	public AudioClip audioClip;
	public float volume;
	 AudioSource fireSound;
	public bool alreadyPlayed = false;

	private float damage = 5f;

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
		fireSound = GetComponent<AudioSource>();
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

    void OnTriggerEnter(	)
    {
		if(!alreadyPlayed)
		{
			fireSound.PlayOneShot(audioClip, volume);
			alreadyPlayed = true;
		}
    }

    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
