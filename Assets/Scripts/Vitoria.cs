using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vitoria : MonoBehaviour {

	public static int colares;

	void OnTriggerEnter(Collider colsior){
		if(colsior.gameObject.CompareTag("Player")){
			if(colares == 3){
				SceneManager.LoadScene("Menu");
			}
		}
	}
}
