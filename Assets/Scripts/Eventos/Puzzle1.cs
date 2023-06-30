using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour {

	public PortaCorrer porta, porta2, porta3;
	public int qtdSimbolos;
	private int posicao, currentSimbolo = 1;
	public List<GameObject> listaSimbolos;
	public List<Transform> listaPosicaoSimbolos;
	public List<Transform> listaPosicao;

	void Start () {
		mudarPosicao ();
	}

	public void mudarPosicao(){
		listaPosicao = new List<Transform>(listaPosicaoSimbolos);
		for (int i = 0; i < listaSimbolos.Count; i++) {
			posicao = Random.Range (0, listaPosicao.Count);
			listaSimbolos [i].transform.position = listaPosicao [posicao].position;
			listaSimbolos [i].transform.rotation = listaPosicao [posicao].rotation;
			listaSimbolos [i].gameObject.SetActive (true);
			listaPosicao.Remove (listaPosicao [posicao]);
		}

	}

		public void conferirOrdem(int idSimboloAtivado){
		if (idSimboloAtivado == currentSimbolo) {
			currentSimbolo++;
		} else {	
			currentSimbolo = 1;
			listaPosicao.Clear ();
			mudarPosicao ();

		}

		if (currentSimbolo == qtdSimbolos + 1) {
			porta.movimentoAutomatico (false, true);
			porta2.movimentoAutomatico (false, true);
			porta3.movimentoAutomatico (true, true);
		}

		}

}
