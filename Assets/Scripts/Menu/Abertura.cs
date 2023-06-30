using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Abertura : MonoBehaviour {

	public GameObject textoInfo;
	public GameObject logo;


	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene ("Menu");
		}	
	}

	public void LigaAnimaTexto(){
		logo.SetActive (false);
		textoInfo.SetActive (true);
	}

	public void TrocaScene(){
		SceneManager.LoadScene ("Menu");
	}
}
