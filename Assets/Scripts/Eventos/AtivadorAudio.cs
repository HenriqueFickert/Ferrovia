using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorAudio : MonoBehaviour {

	public AudioSource audioS;
	public AudioClip audioC;

	void OnTriggerEnter(Collider colisor){
		if (colisor.gameObject.CompareTag("Player")){
			audioS.clip = audioC;
			audioS.PlayOneShot(audioC,1);
			Destroy (gameObject);
		}
	}


}
