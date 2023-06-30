using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaInsanidade : MonoBehaviour {
	private SpawnMonstro spawnScript;
	private Insanidade insanidadeScript;
	private Lanterna lanternaScript;

	void Start(){
		spawnScript = FindObjectOfType<SpawnMonstro> ();
		lanternaScript = FindObjectOfType<Lanterna> ();
		insanidadeScript = FindObjectOfType<Insanidade> ();
	}


	void OnTriggerEnter(Collider colisor){
		if (colisor.gameObject.CompareTag("Player")){
			spawnScript.estaNoComeco = false;
			insanidadeScript.estaNoComeco = false;
			lanternaScript.estaNoTutorial = false;
			Destroy (gameObject);
		}
	}


}
