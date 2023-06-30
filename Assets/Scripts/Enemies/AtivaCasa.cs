using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaCasa : MonoBehaviour {
	SpawnMonstro spawnScript;

	void Start(){
		spawnScript = FindObjectOfType<SpawnMonstro> ();
	}

	void OnTriggerStay(Collider colisor){
		if(colisor.gameObject.CompareTag("Player")){
			spawnScript.estaNumaCasa = true;
		}
	}
	void OnTriggerExit(Collider colisor){
		if(colisor.gameObject.CompareTag("Player")){
			spawnScript.estaNumaCasa = false;
		}
	}

}
