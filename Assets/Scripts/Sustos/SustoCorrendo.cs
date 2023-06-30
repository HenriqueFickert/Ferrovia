using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class SustoCorrendo : MonoBehaviour {

	public AudioClip somSusto;
	private AudioSource audiosS;
	public float velocidadeMovimento = 20;
	private float cronometro;
	public float tempoDestruir;
	public GameObject meshDoMostro;
	private BoxCollider[] Colisores; 
	private bool mover;
	public Transform posicaoAlvo;




	void Start () {		
		audiosS = this.gameObject.GetComponent<AudioSource> ();
		Colisores = gameObject.GetComponents<BoxCollider> ();
	}
	

	void Update () {

		if(mover == true){
			meshDoMostro.transform.position = Vector3.MoveTowards (meshDoMostro.transform.position, posicaoAlvo.position , velocidadeMovimento * Time.deltaTime);
			//transform.Translate (Vector3.forward * Time.deltaTime * velocidadeMovimento);
			cronometro += Time.deltaTime;
		}
		if (cronometro >= tempoDestruir) {
			mover = false;
			meshDoMostro.SetActive (false);
		}


	}


	void OnTriggerEnter(Collider player){

		if (player.gameObject.tag == "Player") {

			foreach (BoxCollider collisores in Colisores) {
				collisores.enabled = false;
				meshDoMostro.SetActive (true);
			}
			audiosS.PlayOneShot (somSusto);
			Destroy (gameObject, somSusto.length);
			mover = true;
		}
	}
}
