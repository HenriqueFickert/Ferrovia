using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveCamera : MonoBehaviour {

	private MoveController playerScript;

	public Transform endPoint;
	public Transform focusPoint;

	public GameObject playerCamera;
	public GameObject cameraScene;

	public GameObject audioL;

	private float timer;

	void Start () {
		playerScript = (MoveController)FindObjectOfType (typeof(MoveController));
	}

	void Update () {
		if (playerScript.isTheCameraActive) {
			cameraScene.SetActive (true);
			playerCamera.SetActive (false);
			audioL.SetActive (true);
			transform.LookAt (focusPoint);
			transform.position = Vector3.Lerp (transform.position, endPoint.position, Time.deltaTime * 0.5f);

			timer += Time.deltaTime;
			if (timer > 5) {
				playerCamera.SetActive (true);
				audioL.SetActive (false);
				cameraScene.SetActive (false);
				playerScript.isTheCameraActive = false;
				timer = 0;
			}
		}
	}
		
}
