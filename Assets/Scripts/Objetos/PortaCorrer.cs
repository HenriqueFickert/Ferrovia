using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PortaCorrer : MonoBehaviour {

	public bool estaTrancada, abrePorEvento, aberta;
	public Inventory inv;

	private Insanidade insanidadeScript;

	private AudioSource audioSoucePorta;
	public AudioClip portaTrancada, portaDestrancando, portaAbrindo, portaFechando;

	private GameObject /*Jogador,*/ inventario;
	public int idChave;
	public float velocidadeAbrir = 10;
	public float tempoParaParar;
	public bool abrePraCima, abrePraDireita, abrePraEsquerda, abrePraFrente, abrePraTras;

	public KeyCode TeclaAbrir = KeyCode.E;
	private bool estaAberta;

	private float cronometro;
	private bool taMovendo = false;

	void Start () {
		insanidadeScript = FindObjectOfType <Insanidade> ();

	//	Jogador = GameObject.FindWithTag ("Player");
	//	inventario = GameObject.FindWithTag ("InventarioItens");
	//	inv = inventario.GetComponent<Inventory> ();
		audioSoucePorta = this.gameObject.GetComponent<AudioSource> ();

		/*if(estaTrancada == true){
			estaAberta = false;
		}*/

		if (aberta == true) {
			taMovendo = true;
			estaAberta = false;
		}

	}
	

	void Update () {

		if (!insanidadeScript.estaMorto){

		if (Sinalizar.ItemOlhado == this.gameObject && estaTrancada == false && Input.GetKeyDown (TeclaAbrir) && taMovendo == false) {
			taMovendo = true;
			if (estaAberta == false) {
				audioSoucePorta.PlayOneShot (portaAbrindo);
			}
			if (estaAberta == true) {
				audioSoucePorta.PlayOneShot (portaFechando);
			}

		}


		//abrindo pra cima
		if (taMovendo == true && abrePraCima == true) {
		
			if (cronometro < tempoParaParar && estaAberta == false) {			
				transform.Translate (Vector3.up * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;		
			}

			if (cronometro >= tempoParaParar && estaAberta == false) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = true;		
			}	

			if (cronometro < tempoParaParar && estaAberta == true) {
				transform.Translate (Vector3.down * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;		
			}
		
			if (cronometro >= tempoParaParar && estaAberta == true) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = false;		
			}
		}

		//porta Abrindo Pra Direita

		if (taMovendo == true && abrePraDireita == true) {

			if (cronometro < tempoParaParar && estaAberta == false) {			
				transform.Translate (Vector3.right * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;
			}

			if (cronometro >= tempoParaParar && estaAberta == false) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = true;
			}

			if (cronometro < tempoParaParar && estaAberta == true) {
				transform.Translate (Vector3.left * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;
			}

			if (cronometro >= tempoParaParar && estaAberta == true) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = false;
			}
		}

		//porta Abrindo Pra Esquerda

		if (taMovendo == true && abrePraEsquerda == true) {

			if (cronometro < tempoParaParar && estaAberta == false) {			
				transform.Translate (Vector3.left * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;
			}

			if (cronometro >= tempoParaParar && estaAberta == false) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = true;
			}

			if (cronometro < tempoParaParar && estaAberta == true) {
				transform.Translate (Vector3.right * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;
			}

			if (cronometro >= tempoParaParar && estaAberta == true) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = false;
			}
		}

		//porta Abrindo pra Frente

		if (taMovendo == true && abrePraFrente == true) {

			if (cronometro < tempoParaParar && estaAberta == false) {			
				transform.Translate (Vector3.forward * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;
			}

			if (cronometro >= tempoParaParar && estaAberta == false) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = true;
			}

			if (cronometro < tempoParaParar && estaAberta == true) {
				transform.Translate (Vector3.back * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;
			}

			if (cronometro >= tempoParaParar && estaAberta == true) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = false;
			}
		}

		//porta abrindo pra tras

		if (taMovendo == true && abrePraTras == true) {

			if (cronometro < tempoParaParar && estaAberta == false) {			
				transform.Translate (Vector3.back * velocidadeAbrir * Time.deltaTime);
				cronometro += Time.deltaTime;
			}

			if (cronometro >= tempoParaParar && estaAberta == false) {
				taMovendo = false;
				cronometro = 0;
				estaAberta = true;
			}

			if (cronometro < tempoParaParar && estaAberta == true) {
				transform.Translate (Vector3.forward * velocidadeAbrir * Time.deltaTime);
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
		if (estaAberta == false) {
			audioSoucePorta.PlayOneShot (portaAbrindo);
		}
		if (estaAberta == true) {
			audioSoucePorta.PlayOneShot (portaFechando);
		}

		estaTrancada = _trancar;

	}
}
