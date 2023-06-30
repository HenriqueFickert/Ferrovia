using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Runner : MonoBehaviour {

	[SerializeField] GameObject monstro;
	[SerializeField] GameObject menuMorte;

	//Nascimento
	public GameObject meshMonstro;
	private bool nasceu;
	private float timerNascimento;
	//Desativamento do monstro
	private float timerDesaparecimento;
	private bool estaSeguindo;

	//scripts
	public SpawnMonstro spawnScript;
	private Insanidade insanidadeScript;

	//Andando
	NavMeshAgent agent;
	public Transform target;
	public Transform eye;
	private float tempoSeguindo;
	public float limiteTempoSeguindo;

	//Particula
	public GameObject particulaDeSombra;
	public GameObject particulaDeSombraDesaparecimento;
	public GameObject particulaDeSombraDesaparecimento2;

	//Animação
	private Animator anim;
	private bool animAndando;

	//Atacando
	private bool acertouPlayer;
	private bool estaAtacando;

	//Audio
	public AudioSource playerAudioS;
	public AudioSource monstroAudioS;
	public AudioSource audioFalas;

	public AudioClip monstroGrito;
	public AudioClip playerGrito;
	private bool limitaSom;
	private bool limitaSom2;


	void Start () {
		//Instancias
		insanidadeScript = FindObjectOfType<Insanidade>();
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		//Nascimento
		timerNascimento = 0;
		nasceu = false;
		meshMonstro.SetActive (false);
		//Desativamento do montro
		timerDesaparecimento = 0;
		estaSeguindo = true;
		tempoSeguindo = 0;
		//Atacando
		estaAtacando = false;
		limitaSom = false;
		limitaSom2 = false;
	}

	void Update () {
		//Nascimento do monstro
		if (!nasceu) {
			timerNascimento += Time.deltaTime;
			if (timerNascimento >= 1.5f) {
				meshMonstro.SetActive (true);
			}
			if (timerNascimento >= 3.2f) {
				particulaDeSombra.SetActive (false);
				timerNascimento = 0;
				nasceu = true;
			}
		}

		//Atacando quando chegar perto
		if (estaSeguindo){
			if (agent.remainingDistance < agent.stoppingDistance) {
				FaceTarget ();
				estaAtacando = true;
				anim.SetLayerWeight (1, 1f);
				anim.SetTrigger ("IsAttacking");
			} 
		}

		//Animações andando e parado
		if (estaSeguindo) {
			animAndando = true;
		} else {
			animAndando = false;
		}
		anim.SetBool ("IsRunning", animAndando);

		//Desativar o monstro
		if (spawnScript.estaNumaCasa) {
			agent.isStopped = true;
			estaSeguindo = false;
			Desaparecer ();
		} else {
			agent.isStopped = false;
			estaSeguindo = true;
		}

		if (!estaAtacando) {
			DesaparecerPeloTempo ();
		}
	}

	void FixedUpdate(){
		if (estaSeguindo) {
			FollowPlayer ();
		}
	}
		

	// ----------------------- SEGUIR JOGADOR -----------------------
	void FollowPlayer(){
		Ray ray = new Ray (eye.position, target.transform.position - eye.position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 200)) {
			Debug.DrawLine (ray.origin, hit.point);
			animAndando = true;
			agent.SetDestination (target.transform.position);
		}
	}
		
	void FaceTarget(){
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5);
	}

	// ----------------------- DESAPARECER -----------------------
	void Desaparecer(){
		timerDesaparecimento += Time.deltaTime;
		if (timerDesaparecimento >= 0.5f) {
			particulaDeSombraDesaparecimento.transform.position = monstro.transform.position;
			particulaDeSombraDesaparecimento.SetActive (true);		
		}
		if (timerDesaparecimento >= 1.5f) {
			timerDesaparecimento = 0;
			monstro.SetActive (false);
		}
	}

	void DesaparecerPeloTempo(){
		tempoSeguindo += Time.deltaTime;
		if (tempoSeguindo >= limiteTempoSeguindo) {
			particulaDeSombra.SetActive (true);
		}
		if (tempoSeguindo >= limiteTempoSeguindo + 2f) {
			particulaDeSombraDesaparecimento2.transform.position = monstro.transform.position;
			particulaDeSombraDesaparecimento2.SetActive (true);	
			monstro.SetActive (false);
			tempoSeguindo = 0;
		}
	}
		
	// ----------------------- ATACAR O JOGADOR -----------------------
	void OnTriggerStay(Collider colisor){
		if (colisor.gameObject.CompareTag ("Player")) {
			acertouPlayer = true;
		}
	}

	void OnTriggerExit (Collider colisor){
		if (colisor.gameObject.CompareTag ("Player")) {
			acertouPlayer = false;
		}
	}
		
	public void MortePlayer(){
		if (acertouPlayer) {
			if (!limitaSom) {
				playerAudioS.clip = playerGrito;
				playerAudioS.PlayOneShot (playerGrito);
				limitaSom = true;
			}
			audioFalas.clip = null;
			insanidadeScript.estaMorto = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			menuMorte.SetActive (true);
			Time.timeScale = 0;
		}
	}

	// ----------------------- SOM ATAQUE -----------------------
	public void SomAtaque(){
		if (!menuMorte.activeSelf) {
			if (!limitaSom2) {
				monstroAudioS.clip = monstroGrito;
				monstroAudioS.PlayOneShot (monstroGrito);
				limitaSom2 = true;
			}
		}
	}

	public void AtivaSomAtaque(){
		limitaSom2 = false;
	}


}
