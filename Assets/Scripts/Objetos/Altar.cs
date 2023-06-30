using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour {

	public GameObject particula, particula2;
	public AudioSource efeitoDestruicao, audioMonstro;
	public AudioClip somDestruicao, somMonstro;
	public Inventory invItens;
	public string nomeColar;
	public GameObject[] parteAltares;
	public KeyCode TeclaAbrir = KeyCode.E;

	void Update(){		
		if (Sinalizar.ItemOlhado == this.gameObject && Input.GetKeyDown (TeclaAbrir)) {
			PlayerManager.altaresDestruidos++;
			particula.SetActive (true);
			efeitoDestruicao.PlayOneShot (somDestruicao);
			audioMonstro.PlayOneShot (somMonstro);
			for (int i = 0; i < parteAltares.Length; i++) {
				parteAltares [i].SetActive (false);
			}
			invItens.AddItens (nomeColar, 1);
			PlayerCollectable.qtdNot++;
			PlayerCollectable.itensNot++;
			GetComponent<SphereCollider> ().enabled = false;
			Vitoria.colares++;
			StartCoroutine ("destruir");
		}
	}


	IEnumerator destruir(){
		yield return new WaitForSeconds (0.1f);
		particula2.SetActive (true);
		yield return new WaitForSeconds (11f);
		Destroy (gameObject);
	}

}
