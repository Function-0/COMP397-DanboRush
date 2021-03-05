using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float lookRadius = 10f;

	public float health = 100f; // enemy hp

	Transform target;
	NavMeshAgent agent;

	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	private bool _alive;
	//CharacterCombat combatManager;

	public GameObject player;

//	public GameObject ThrowBoxPrefab;
	//public Camera playerCamera;
	//private GameObject ThrowBox;

	public PlayerBehaviour playerBehaviour;

	public HealthBarScreenSpaceController healthBar;

	void Start()
	{
		player = GameObject.Find("Amazon danbo");
		target = player.transform;
		agent = GetComponent<NavMeshAgent>();
		playerBehaviour = FindObjectOfType<PlayerBehaviour>();
		//_alive = true;
		//combatManager = GetComponent<CharacterCombat>();
	}

	void Update()
	{
		// Get the distance to the player
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the radius
		if (distance <= lookRadius)
		{
			// Move towards the player
			agent.SetDestination(target.position);

			if (distance <= agent.stoppingDistance)
			{
				// Attack
				//playerBehaviour.TakeDamage(0.1);
				agent.SetDestination(target.position);
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
