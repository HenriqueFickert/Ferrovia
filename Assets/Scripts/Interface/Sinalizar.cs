using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sinalizar : MonoBehaviour {


	public Text infoText, nameInfo, tutorialText;
	public float disRay = 2.5f;
	public LayerMask layerMask;
	Ray ray;

	public static GameObject ItemOlhado;

	private float distancia;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		DetectarOlhar ();

	}


	void DetectarOlhar(){


		ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hit;

		if (ItemOlhado != null) {

			distancia = Vector3.Distance (transform.position, ItemOlhado.transform.position);

		}
		if (distancia > 5) {

			ItemOlhado = null;
			nameInfo.text = "";
			infoText.text = "";

		}

	
		if (Physics.Raycast (ray, out hit, disRay)) {			

			if (hit.collider.tag == "Portas") {

				ItemOlhado = hit.collider.gameObject;

				if (nameInfo.enabled == true) {					
					nameInfo.text = "Porta";
					infoText.text = "Pressione 'E' para abrir ou fechar.";
					//tutorialText.text = "";
				}
			} else if (hit.collider.tag == "Mapa") {
				ItemOlhado = hit.collider.gameObject;
				nameInfo.text = hit.collider.name;
				infoText.text = "Pressione 'E' para interagir.";
				//tutorialText.text = "";

			} else if (hit.collider.tag == "Item") {
				ItemOlhado = hit.collider.gameObject;
				nameInfo.text = hit.collider.name;
				infoText.text = "Pressione 'E' para coletar.";
				//tutorialText.text = "";

			}else if (hit.collider.tag == "Simbolos") {
				ItemOlhado = hit.collider.gameObject;
				nameInfo.text = "Simbolos Misticos";
				infoText.text = "Pressione 'E' para Interagir.";
				//tutorialText.text = "";

			}else if (hit.collider.tag == "Altar") {
				ItemOlhado = hit.collider.gameObject;
				nameInfo.text = "Altar";
				infoText.text = "Pressione 'E' para Destrui-lo.";
				//tutorialText.text = "";

			}else if (hit.collider.tag == "Gaveta") {
				ItemOlhado = hit.collider.gameObject;
				nameInfo.text = "Gaveta";
				infoText.text = "Pressione 'E' para abrir ou fechar.";
				//tutorialText.text = "";

			} else {
				ItemOlhado = null;
				nameInfo.text = "";
				infoText.text = "";
			}
		}

	}

}
