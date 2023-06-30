using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class Susto1 : MonoBehaviour {

	private BoxCollider[] Colisores; 
	private AudioSource audiosS;
	public AudioClip audioSusto;
	private float cronometro;
	public float tempoImage;
	private bool contar;
	private int randomSusto;
	public bool temSom;
	public GameObject MonstroSkin;
	public bool sustoObrigatorio;

	void Start () {	

		audiosS = this.gameObject.GetComponent<AudioSource> ();
		randomSusto = Random.Range (1,3);


		if(randomSusto != 1 && sustoObrigatorio == false){
			Destroy (gameObject);
		}

		MonstroSkin.SetActive (false);
		Colisores = gameObject.GetComponents<BoxCollider> ();

	}
	

	void Update () {


		if (contar == true) {
			cronometro += Time.deltaTime;
		}
		if (cronometro >= tempoImage) {
			contar = false;
			MonstroSkin.SetActive (false);

		}

	}


	void OnTriggerEnter(Collider player){

		if (player.gameObject.tag == "Player") {

			contar = true;
			MonstroSkin.SetActive (true);

			if (temSom == true) {		
				audiosS.PlayOneShot (audioSusto);		
			}
			foreach (BoxCollider collisores in Colisores) {
				collisores.enabled = false;		
			}

			Destroy (gameObject, audioSusto.length);


		}

	}

}
