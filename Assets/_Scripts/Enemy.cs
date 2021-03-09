using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float lookRadius = 10f;
	public GameObject boxPrefab;
	public float health = 100f; // enemy hp
	public float shootingDistance = 10f;

	public float fireRate = 1f;
	private float nextTimeToFire = 0f;

	Transform target;
	NavMeshAgent agent;

	public float range = 100f;

	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	private bool _alive;

	//CharacterCombat combatManager;

	private GameObject player;

	private float distance;

	//private PlayerBehaviour playerBehaviour;

	public HealthBarScreenSpaceController healthBar;

	void Start()
	{
		player = GameObject.Find("Amazon danbo");
		target = player.transform;
		agent = GetComponent<NavMeshAgent>();
		//playerBehaviour = FindObjectOfType<PlayerBehaviour>();

	}

	void Update()
	{
		// Get the distance to the player
		distance = Vector3.Distance(target.position, transform.position);

		// If inside the radius
		if (distance <= lookRadius)
		{
			// Move towards the player
			agent.SetDestination(target.position);
		}

		if (distance <= shootingDistance && Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			GameObject boxObject = Instantiate(boxPrefab);
			boxObject.transform.position = transform.position;
			boxObject.transform.forward = transform.forward;
			shoot();
		}

	}

	void shoot()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, range))
		{
			Debug.Log(hit.transform.name);

			PlayerBehaviour player = hit.transform.GetComponent<PlayerBehaviour>();
			if (player != null)
			{
				player.TakeDamage(10f);
			}
		}
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
	// killing enemy
	public void TakeDamage(float amount)
	{
		health -= amount;
		healthBar.TakeDamage(amount);
		if (health <= 0f)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
