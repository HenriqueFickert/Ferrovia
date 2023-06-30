using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simbolos : MonoBehaviour {

	public KeyCode TeclaInteragir = KeyCode.E;
	public int idSimbolo;
	private Puzzle1 puzzleScript;

	void Start () {
		puzzleScript = GetComponentInParent<Puzzle1> ();
	}
	

	void Update () {

		if (Sinalizar.ItemOlhado == this.gameObject && Input.GetKeyDown (TeclaInteragir)) {
			gameObject.SetActive (false);
			puzzleScript.conferirOrdem (idSimbolo);
		}
	}
}
