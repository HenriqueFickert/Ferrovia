using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raios : MonoBehaviour {

	[SerializeField] Light luz;

	private int contadorSom;
	private int contadorTrovao;
	public AudioClip[] somTrovao;
	public AudioSource audioTrovao;


	private float timer;
	private float timerRaio;
	private int randomRaio;
	private int randomTimerRaio;

	void Start () {
		randomTimerRaio = Random.Range (10, 35);
	}
	

	void Update () {
		timer += Time.deltaTime;
		if (timer > randomTimerRaio) {
			randomRaio = Random.Range (1, 4);
			contadorTrovao = Random.Range (0, 6);
			contadorSom = 0;
			randomTimerRaio = Random.Range (10, 35);
			timer = 0;
		}
			
		if (randomRaio == 0) {
			luz.intensity = 0;
		}
		else if (randomRaio == 1) {
			Raio1 ();
		} else if (randomRaio == 2) {
			Raio2 ();
		} else if (randomRaio == 3) {
			Raio3 ();
		}
		
	}

	void Raio1(){
		if (contadorSom < 1) {
			audioTrovao.clip = somTrovao [contadorTrovao];
			audioTrovao.PlayOneShot (somTrovao [contadorTrovao], 1);
			contadorSom++;
		}

		timerRaio += Time.deltaTime;
		if (timerRaio >= 0.1) {
			luz.intensity = 0.6f;
		}
		if (timerRaio >= 0.2) {
			luz.intensity = 0.0f;
		}
		if (timerRaio >= 0.3) {
			luz.intensity = 0.3f;
		}
		if (timerRaio >= 0.4) {
			luz.intensity = 0.5f;
		}
		if (timerRaio >= 0.5) {
			luz.intensity = 0.2f;
		}
		if (timerRaio >= 0.6) {
			luz.intensity = 0.0f;
			randomRaio = 0;
			timerRaio = 0;
		}
	}
	void Raio2(){


		if (contadorSom < 1) {
			audioTrovao.clip = somTrovao [contadorTrovao];
			audioTrovao.PlayOneShot (somTrovao [contadorTrovao], 1);
			contadorSom++;
		}


		timerRaio += Time.deltaTime;

		if (timerRaio >= 0.1) {
			luz.intensity = 0.7f;
		}
		if (timerRaio >= 0.2) {
			luz.intensity = 0.0f;
		}
		 if (timerRaio >= 0.3) {
			luz.intensity = 0.5f;
		}
		if (timerRaio >= 0.4) {
			luz.intensity = 0.0f;
		}
		if (timerRaio >= 0.5) {
			luz.intensity = 0.6f;
		}
		if (timerRaio >= 0.6) {
			luz.intensity = 0.2f;
		}
		if (timerRaio >= 0.7) {
			luz.intensity = 0.4f;
		}
		if (timerRaio >= 0.8) {
			luz.intensity = 0.0f;
			randomRaio = 0;
			timerRaio = 0;
		}
	}
	void Raio3(){

		if (contadorSom < 1) {
			audioTrovao.clip = somTrovao [contadorTrovao];
			audioTrovao.PlayOneShot (somTrovao [contadorTrovao], 1);
			contadorSom++;
		}

		timerRaio += Time.deltaTime;
		if (timerRaio >= 0.1) {
			luz.intensity = 0.2f;
		}
		if (timerRaio >= 0.2) {
			luz.intensity = 0.0f;
		}
		if (timerRaio >= 0.3) {
			luz.intensity = 0.5f;
		}
		if (timerRaio >= 0.4) {
			luz.intensity = 0.0f;
		}
		if (timerRaio >= 0.5) {
			luz.intensity = 0.6f;
		}
		if (timerRaio >= 0.6) {
			luz.intensity = 0.2f;
		}
		if (timerRaio >= 0.7) {
			luz.intensity = 0.4f;
		}
		if (timerRaio >= 0.8) {
			luz.intensity = 0.0f;
			randomRaio = 0;
			timerRaio = 0;
		}

	}


}
