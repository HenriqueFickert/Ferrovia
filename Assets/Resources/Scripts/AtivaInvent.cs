using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtivaInvent : MonoBehaviour {


	[HideInInspector]public GameObject invItens, invCartas, invCanvas;
	[HideInInspector]public static bool invOn = true;


	public KeyCode TeclaAbrirInv = KeyCode.Q;

	public AudioSource mochila;
	public AudioClip abrindoMochila;

	private Insanidade insanidadeScript;

	void Awake(){


		invCartas = GameObject.Find("invCartas");
		invItens = GameObject.Find("invItens");
		invCanvas = GameObject.Find ("invCanvas");


	}

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		insanidadeScript = FindObjectOfType<Insanidade> ();
		invCartas.SetActive (false);
		invCanvas.SetActive (false);
	}
	

	void Update () {

		/*if (invOn && !PauseMenu.isPaused) {

			if (Input.GetKeyDown (TeclaAbrirInv)) {

				PlayerCollectable.qtdNot = 0;

				if (invCanvas.activeSelf == true) {
					mochila.PlayOneShot (abrindoMochila, 0.5f);
					invCanvas.SetActive (false);
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;

				} else if (invCanvas.activeSelf == false) {
					mochila.PlayOneShot (abrindoMochila, 0.5f);
					invCanvas.SetActive (true);		
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.None;
				}
			}
		}*/

		if (insanidadeScript.estaMorto) {
			invCanvas.SetActive (false);
		}

		if (invOn && !PauseMenu.isPaused) {			
			if (invCanvas.activeSelf == true) {
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				if (Input.GetKeyDown (TeclaAbrirInv)) {
					mochila.PlayOneShot (abrindoMochila, 0.5f);
					invCanvas.SetActive (false);
				}
			} else if (invCanvas.activeSelf == false && !insanidadeScript.estaMorto) {
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				if (Input.GetKeyDown (TeclaAbrirInv)) {
					mochila.PlayOneShot (abrindoMochila, 0.5f);
					invCanvas.SetActive (true);	
				}
			}

			}
	}

}
