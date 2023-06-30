using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour {
	NavMeshAgent agent;
	bool patrolling;
	public Transform[] patrolTargets;
	private int destPoint;
	bool arrived;

	//Animator anim;
	public Transform target;
	Vector3 lastKnowPosition;
	public Transform eye;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	//	anim = GetComponent<Animator> ();
		lastKnowPosition = transform.position;
	}


	bool CanSeeTarget(){
		
		bool canSee = false;
		Ray ray = new Ray (eye.position, target.transform.position - eye.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 20)) {
			//Debug.DrawLine (ray.origin, hit.point);
			if (hit.transform != target) {
				canSee = false;
			} else {
				lastKnowPosition = target.transform.position;
				canSee = true;
			}
		}
		Debug.DrawLine (ray.origin, hit.point);
		return canSee;
	
	}


	void Update () {
		if (agent.pathPending) {
			return;
		}
		if (patrolling) {
			if (agent.remainingDistance < agent.stoppingDistance) {
				if (!arrived) {
					arrived = true;
					StartCoroutine ("GoToNextPoint");
				}
			} else {
				arrived = false;
			}
		}

		if (CanSeeTarget ()) {
			agent.SetDestination (target.transform.position);
			patrolling = false;
			if (agent.remainingDistance < agent.stoppingDistance) {
				//Atacar - anim.Setbool ("Attack", true);
			} else {
				//Parar de atacar - anim.Setbool ("Attack", false);
			}
		}
		else {
			//parar de atacar - - anim.Setbool ("Attack", false);
			if (!patrolling){
				agent.SetDestination (lastKnowPosition);
				if (agent.remainingDistance < agent.stoppingDistance) {
					patrolling = true;
					StartCoroutine ("GoToNextPoint");
				}
			}
		}
		//anim.setfloat ("Foward", agent.velocity.sqrMagnitude;
	}


	IEnumerator GoToNextPoint(){
		if (patrolTargets.Length == 0) {
			yield break;
		}
		patrolling = true;
		yield return new WaitForSeconds (2f);
		arrived = false;
		agent.destination = patrolTargets [destPoint].position;
		destPoint = (destPoint + 1) % patrolTargets.Length;
	}


	void OnTriggerEnter(Collider colisor){
		
	}

}
