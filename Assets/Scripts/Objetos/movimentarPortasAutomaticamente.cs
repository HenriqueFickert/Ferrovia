using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentarPortasAutomaticamente : MonoBehaviour {

	public bool portaComunBool, trancar, aberta;
	public PortaComum portaComum;
	public PortaCorrer portaCorrer;
	public List<GameObject> listaColliders;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			if(portaComunBool){
				portaComum.movimentoAutomatico (trancar, aberta); 
			}else if(!portaComunBool){
				portaCorrer.movimentoAutomatico (trancar, aberta); 
			}
			for (int i = 0; i < listaColliders.Count; i++) {
				Destroy (listaColliders [i]);
			}

		}

	}
}
