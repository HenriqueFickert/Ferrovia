using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

	public float lookRadius = 10;

	Transform target;
	NavMeshAgent agent;
	public Transform[] patrolTargets;
	private bool isPatrolling;
	private int destPoint;

	private bool isSeeingThePlayer;

	void Start () {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent> ();
		isPatrolling = true;
	}
	

	void Update () {

		if (isPatrolling) {
			if (agent.remainingDistance < agent.stoppingDistance) {
				agent.destination = patrolTargets [destPoint].position;
				destPoint = (destPoint + 1) % patrolTargets.Length;
			}
		}

		float distance = Vector3.Distance (target.position, transform.position);
		if (distance <= lookRadius) {
			agent.speed = 10;
			agent.angularSpeed = 1000;
			agent.SetDestination (target.position);
			if (distance <= agent.stoppingDistance) {
				FaceTarget ();
			}
		} else {
			agent.speed = 5;
			agent.angularSpeed = 200;
		}

		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.forward);
		if (Physics.Raycast (ray, out hit, 20)) {
			if (hit.collider.gameObject.CompareTag ("Player")) {
				isSeeingThePlayer = true;
			}
		}

		if (isSeeingThePlayer) {
			FollowPlayer ();
		}

	}

	void FollowPlayer(){
		Ray ray = new Ray (transform.position, target.transform.position - transform.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			isPatrolling = false;
			agent.speed = 10;
			agent.angularSpeed = 1000;
			Debug.DrawLine (ray.origin, hit.point);
			agent.SetDestination (target.transform.position);
		}
	}

	void FaceTarget(){
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 10f);
	}

	void OnDrawGizmosSelected (){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}
}
