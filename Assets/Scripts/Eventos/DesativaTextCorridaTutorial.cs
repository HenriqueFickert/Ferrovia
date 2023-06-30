using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesativaTextCorridaTutorial : MonoBehaviour {

	public Text txtTutorial;
	public GameObject colider;

	void OnTriggerEnter(Collider colisor){
		if (colisor.gameObject.CompareTag ("Player")) {
			txtTutorial.text = "";
			colider.SetActive (false);
		}

	}
}
