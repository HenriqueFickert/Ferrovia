using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PortaComum : MonoBehaviour {

	private Insanidade insanidadeScript;

	public bool estaTrancada, abrePorEvento, aberta;
	public Inventory inv;

	private AudioSource audioSoucePorta;
	public AudioClip portaTrancada, portaDestrancando, portaAbrindo, portaFechando;

	private GameObject /*Jogador,*/ inventario;
	public int idChave;
	public float velocidadeRotacionar = 20;
	public float tempoParaParar;
	public bool inverter;

	public KeyCode TeclaAbrir = KeyCode.E;
	[HideInInspector]public bool estaAberta = false;

	private float cronometro;
	private bool taMovendo = false;

	void Start () {
		insanidadeScript = FindObjectOfType <Insanidade> ();

	//	Jogador = GameObject.FindWithTag ("Player");
     	//inventario = GameObject.FindWithTag ("InventarioItens");
    	//inv = inventario.GetComponent<Inventory> ();
		audioSoucePorta = this.gameObject.GetComponent<AudioSource> ();


		/*if(estaTrancada == true){			
			estaAberta = false;
		}*/

		if (aberta == true) {
			taMovendo = true;
			estaAberta = false ;
		}

	}
	void Update () {
		if (!insanidadeScript.estaMorto){

		//distancia = Vector3.Distance (Jogador.transform.position, transform.position);
		if (Sinalizar.ItemOlhado == this.gameObject && estaTrancada == false && Input.GetKeyDown (TeclaAbrir) && taMovendo == false) {
			taMovendo = true;
			if (estaAberta == false) {
				audioSoucePorta.PlayOneShot (portaAbrindo);
			}
			if (estaAberta == true) {
				audioSoucePorta.PlayOneShot (portaFechando);
			}

		}
			

		if (taMovendo == true && inverter == false) {

			if (cronometro < tempoParaParar && estaAberta == false) {			
				transform.Rotate (Vector3.up, velocidadeRotacionar * Time.deltaTime);
				cronometro += Time.deltaTime;		
			}

			if (cronometro >= tempoParaParar && estaAberta == false) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = true;		
			}	

			if (cronometro < tempoParaParar && estaAberta == true) {
				transform.Rotate (Vector3.down, velocidadeRotacionar * Time.deltaTime);
				cronometro += Time.deltaTime;		
			}

			if (cronometro >= tempoParaParar && estaAberta == true) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = false;		
			}

		}


		if (taMovendo == true && inverter == true) {

			if (cronometro < tempoParaParar && estaAberta == false) {			
				transform.Rotate (Vector3.down, velocidadeRotacionar * Time.deltaTime);
				cronometro += Time.deltaTime;		
			}

			if (cronometro >= tempoParaParar && estaAberta == false) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = true;		
			}	

			if (cronometro < tempoParaParar && estaAberta == true) {
				transform.Rotate (Vector3.up, velocidadeRotacionar * Time.deltaTime);
				cronometro += Time.deltaTime;		
			}

			if (cronometro >= tempoParaParar && estaAberta == true) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = false;		
			}

		}

		if (Sinalizar.ItemOlhado == this.gameObject && estaTrancada == true && Input.GetKeyDown (TeclaAbrir) && abrePorEvento == false) {					
			bool C = inv.procuraSlotComItem (idChave);
			if (C == true) {
				audioSoucePorta.PlayOneShot (portaDestrancando);
				estaTrancada = false;
			} else
				audioSoucePorta.PlayOneShot (portaTrancada);

		}
		if (Sinalizar.ItemOlhado == this.gameObject && estaTrancada == true && Input.GetKeyDown (TeclaAbrir) && abrePorEvento == true) {

			audioSoucePorta.PlayOneShot (portaTrancada);


		}


	}
	}


	public void movimentoAutomatico(bool _trancar, bool _aberta){
		if(_aberta != estaAberta){
			taMovendo = true;
		}
		if(_trancar){
			estaTrancada = true;
		}
		if (!_trancar) {
			estaTrancada = false;
		}

		if (estaAberta == false) {
			audioSoucePorta.PlayOneShot (portaAbrindo);
		}
		if (estaAberta == true) {
			audioSoucePorta.PlayOneShot (portaFechando);
		}

	}

}
