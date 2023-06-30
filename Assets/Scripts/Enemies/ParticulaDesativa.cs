using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaDesativa : MonoBehaviour {

	public GameObject particula;
	private float timer;

	void Start () {
		timer = 0;
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer >= 5) {
			particula.SetActive (false);
			timer = 0;
		}
	}
}
