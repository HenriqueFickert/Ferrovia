using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnLantern : MonoBehaviour {


	private Insanidade insanidadeScript;

	public bool isOn;

	public AudioSource audioSLiga;
	public AudioSource audioSDesliga;
	public AudioClip audioLiga;


	public GameObject lanterna;

	void Start () {
		//isOn = true;
		insanidadeScript = GetComponent<Insanidade>();
	}
	

	void Update () {
		if (!PauseMenu.isPaused && !insanidadeScript.estaMorto && Input.GetKeyDown (KeyCode.F)) {
			isOn = !isOn;
			if (isOn) {
				audioSLiga.clip = audioLiga;
				audioSLiga.PlayOneShot (audioLiga, 0.1f);
			} else {
				audioSDesliga.clip = audioLiga;
				audioSDesliga.PlayOneShot (audioLiga,0.1f);
			}
		}
	
		if (isOn) {
			lanterna.SetActive (true);
		} else {
			lanterna.SetActive (false);
		}

	}
}
