using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mapa : MonoBehaviour {

	public GameObject mapaUI;

	private bool estaNoRange;
	private bool mapaAtivo;
	void Update(){
		if (Sinalizar.ItemOlhado == this.gameObject && Input.GetKeyDown (KeyCode.E)) {
			mapaAtivo = !mapaAtivo;	
		} else if (!Sinalizar.ItemOlhado == this.gameObject) {
			mapaAtivo = false;
		}

		if (mapaAtivo) {
			mapaUI.SetActive (true);
		} else {
			mapaUI.SetActive (false);
		}
	}

	/*void OnTriggerStay(Collider colisor){
		if (colisor.gameObject.CompareTag ("Player")) {
			estaNoRange = true;
		} else {
			estaNoRange = false;
		}
	}

	void OnTriggerExit(Collider colisor){
		if (colisor.gameObject.CompareTag ("Player")) {
			estaNoRange = false;
			mapaAtivo = false;
			mapaUI.SetActive (false);
		}
	}*/

}
