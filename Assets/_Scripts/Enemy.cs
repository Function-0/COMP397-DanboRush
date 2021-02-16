using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public float lookRadius = 10f;

	Transform target;
	NavMeshAgent agent;

	public float speed = 3.0f;
	public float obstacleRange = 5.0f;
	private bool _alive;
	//CharacterCombat combatManager;

	public GameObject player;

	public GameObject ThrowBoxPrefab;
	private GameObject ThrowBox;

	void Start()
	{
		player = GameObject.Find("Amazon danbo");
		target = player.transform;
		agent = GetComponent<NavMeshAgent>();
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

            //if (distance <= agent.stoppingDistance)
            //{
            //    // Attack
            //    //combatManager.Attack(Player.instance.playerStats);
            //    //FaceTarget();
            //}
        }

		//Ray ray = new Ray(transform.position, transform.forward);
		//RaycastHit hit;
		//if (Physics.SphereCast(ray, 0.75f, out hit))
		//{
		//	GameObject hitObject = hit.transform.gameObject;
		//	if (hitObject.GetComponent<PlayerInfo>())
		//	{
		//		if (ThrowBox == null)
		//		{
		//			ThrowBox = Instantiate(ThrowBoxPrefab) as GameObject;
		//			ThrowBox.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
		//			ThrowBox.transform.rotation = transform.rotation;
		//		}
		//	}
		//	else if (hit.distance < obstacleRange)
		//	{
		//		float angle = Random.Range(-110, 110);
		//		transform.Rotate(0, angle, 0);
		//	}
		//}
	}

	
	//public void SetAlive(bool alive) {
	//	_alive = alive;
	//}

    // Point towards the player
    //void FaceTarget()
    //{
    //    Vector3 direction = (target.position - transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    //}

    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
