using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorCamera : MonoBehaviour {

	public GameObject jogoDeCamera;

	private MoveController playerController;

	void Start () {
		playerController = FindObjectOfType<MoveController> ();
	}
	

	void OnTriggerEnter(Collider colisor){
		if (colisor.gameObject.CompareTag("Player")){
			jogoDeCamera.SetActive(true);
			//MoveController.movementOn = false;
			playerController.isTheCameraActive = true;
			Destroy (gameObject);
		}
	}
}
