using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	private MoveController moveController;

	[SerializeField] private AudioClip [] grass_steps;
	[SerializeField] private AudioClip [] hard_steps;
	[SerializeField] private AudioClip [] water_steps;
	[SerializeField] private AudioClip [] wood_steps;
	[SerializeField] private AudioClip [] ground_steps;
	[SerializeField] private AudioSource step_source;

	int r;

	public float distToGround = 1.3f;

	RaycastHit hitInfo;


	bool Grounded() {
		return Physics.Raycast(transform.position, -Vector3.up, out hitInfo, distToGround);
	}

	void Start(){
		moveController = GetComponent<MoveController> ();
		r = Random.Range(0, grass_steps.Length);
	}

	void Update(){
		FoodSteps ();
	}

	public void FoodSteps() {
		if (Grounded()) {
			switch (hitInfo.transform.GetComponent<Collider>().tag) {
			case "HardFloor":
				moveController.isOnGround = 0;
				break;
			case "GrassFloor":
				moveController.isOnGround = 1;
				break;
			case "WoodFloor":
				moveController.isOnGround = 2;
				break;
			case "WaterFloor":
				moveController.isOnGround = 3;
				break;
			case "GroundFloor":
				moveController.isOnGround = 4;
				break;
			default:
				moveController.isOnGround = 0;
				break;
			}
		}
	}

	public void PlayGrassStep(){
		r = Random.Range(0, grass_steps.Length);
		step_source.PlayOneShot (grass_steps[r]);
	}

	public void PlayHardStep(){
		r = Random.Range(0, grass_steps.Length);
		step_source.PlayOneShot(hard_steps[r]);
	}

	public void PlayWoodStep(){
		r = Random.Range(0, grass_steps.Length);
		step_source.PlayOneShot(wood_steps[r]);
	}

	public void PlayWaterStep(){
		r = Random.Range(0, grass_steps.Length);
		step_source.PlayOneShot (water_steps[r]);
	}
	public void PlayGroundStep(){
		r = Random.Range(0, grass_steps.Length);
		step_source.PlayOneShot (ground_steps[r]);
	}


}
