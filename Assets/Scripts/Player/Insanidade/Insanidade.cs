using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
//using UnityEngine.PostProcessing;

public class Insanidade : MonoBehaviour {

	SpawnMonstro spawnScript;

	TurnOnLantern turnOnLantern;
	AtributosPlayer attribute;
	MoveController moveController;
	MotionBlur blur;
	VignetteAndChromaticAberration vig;

	public AudioClip somMorte;
	private float timerDelayMorte;
	private int contadorSom;

	public AudioSource audioS;
	public AudioClip whispers;
	private float timerAudioWhispers;


	private float timerInsanity;

	private float timerBlur;
	private float timerChroma;
	private float timerWalkSpeed;
	private float timerRunSpeed;

	private float timerStarter;

	public bool estaNoComeco;

	public GameObject menuMorte;
	public bool estaMorto;

	void Start () {
		estaNoComeco = true;
		spawnScript = GetComponentInChildren<SpawnMonstro> ();
		vig = GetComponentInChildren <VignetteAndChromaticAberration> ();
		blur = GetComponentInChildren<MotionBlur> ();

		turnOnLantern = GetComponent<TurnOnLantern> ();
		attribute = GetComponent<AtributosPlayer> ();
		moveController = GetComponent<MoveController> ();
		attribute.insanity = 0;
	}

	void Update () {
		//print (attribute.insanity);
		if (!estaNoComeco) {
			if (!moveController.isTheCameraActive) {
				if (attribute.currentBatery <= 0 || !turnOnLantern.isOn) {
					timerStarter += Time.deltaTime;
					if (timerStarter >= 2) {
						timerInsanity += Time.deltaTime;
						if (timerInsanity >= 1) {
							for (int i = 0; i < 1; i++) {
								attribute.insanity++;
							}
							timerInsanity = 0;
						}
					}
				} else {
					timerStarter = 0;
				}
			}
		}

		//QUEDA DE INSANIDADE
		/*if (attribute.insanity > 0 && turnOnLantern.isOn) {
			timerInsanity += Time.deltaTime;
			if (timerInsanity >= 1) {
				for (int i = 0; i < 1; i++) {
					attribute.insanity--;
				}
				timerInsanity = 0;
			}
		} */
			

		//EFEITOS DE INSANIDADE
		if (attribute.insanity >= 0 && attribute.insanity < 25) {
			Percent0 ();
		} else if (attribute.insanity >= 25 && attribute.insanity < 50) {
			Percent25 ();
		} else if (attribute.insanity >= 50 && attribute.insanity < 75) {
			Percent50 ();
		} else if (attribute.insanity >= 75 && attribute.insanity < 85) {
			Percent75 ();
		}else if (attribute.insanity >= 85) {
			Percent100 ();
		}

	}

	void Percent0(){
		//SPAWN MONSTRO
		spawnScript.rateSpawn = 15;

		//SOM-VOZES (DIMINUI)
		timerAudioWhispers += Time.deltaTime;
		if (timerAudioWhispers >= 0.25) {
			if (audioS.volume > 0) {
				audioS.volume -= 0.05f;
				timerAudioWhispers = 0;
			}
		}

		//BLUR(DIMINUI)
		timerBlur += Time.deltaTime;
		if (blur.blurAmount > 0) {
			if (timerBlur >= 0.5) {
				blur.blurAmount -= 0.05f;
				timerBlur = 0;
			}
		}

		//CHROMA ABRRATION (DIMINUI)
		timerChroma += Time.deltaTime;
		if (vig.chromaticAberration > 0) {
			if (timerChroma >= 0.5) {
				vig.chromaticAberration--;
				timerChroma = 0;
			}
		}

		//ARRUMA A VELOCIDADE DO PLAYER
		moveController.moveSpeed = 4;
		moveController.runSpeed = 10;
		moveController.step_interval = 0.7f;

	}

	void Percent25(){
		//SPAWN MONSTRO
		spawnScript.rateSpawn = 13;


		//SOM-VOZES (AUMENTA E DIMINUI)
		timerAudioWhispers += Time.deltaTime;
		if (audioS.volume < 0.25) {
			if (timerAudioWhispers >= 0.25) {
				audioS.volume += 0.05f;
				timerAudioWhispers = 0;
			}
		} else if (audioS.volume > 0.25) {
			if (timerAudioWhispers >= 0.25) {
				audioS.volume -= 0.05f;
				timerAudioWhispers = 0;
			}
		}
	
		//BLUR (AUMENTA E DIMINUI)
		timerBlur += Time.deltaTime;
		if (blur.blurAmount <= 0.3) {
			if (timerBlur >= 0.5) {
				blur.blurAmount += 0.05f;
				timerBlur = 0;
			}
		} else if (blur.blurAmount >= 0.35){
			if (timerBlur >= 0.5) {
				blur.blurAmount -= 0.05f;
				timerBlur = 0;
			}
		}

		//CHROMA ABERRATION (DIMINUI)
		timerChroma += Time.deltaTime;
		if (vig.chromaticAberration > 0) {
			if (timerChroma >= 0.5) {
				vig.chromaticAberration--;
				timerChroma = 0;
			}
		}


		//VELOCIDADE DO PLAYER - MELHORA
		timerWalkSpeed += Time.deltaTime;
		if (timerWalkSpeed >= 0.5) {
			if (moveController.moveSpeed < 4) {
				moveController.moveSpeed++;
				if (moveController.step_interval > 0.7) {
					moveController.step_interval -= 0.1f;
				}
				timerWalkSpeed = 0;
			}	
		}

		timerRunSpeed += Time.deltaTime;
		if (timerRunSpeed >= 0.5) {
			if (moveController.runSpeed < 10) {
				moveController.runSpeed++;
				timerRunSpeed = 0;
			}	
		}

	}

	void Percent50(){
		//SPAWN MONSTRO
		spawnScript.rateSpawn = 12;

		//SOM-VOZES (AUMENTA E DIMINUI)
		timerAudioWhispers += Time.deltaTime;
		if (audioS.volume <= 0.5) {
			if (timerAudioWhispers >= 0.25) {
				audioS.volume += 0.05f;
				timerAudioWhispers = 0;
			}
		} else if (audioS.volume >= 0.55) {
			if (timerAudioWhispers >= 0.25) {
				audioS.volume -= 0.05f;
				timerAudioWhispers = 0;
			}
		}

		//BLUR (AUMENTA E DIMINUI)
		timerBlur += Time.deltaTime;
		if (blur.blurAmount <= 0.6) {
			if (timerBlur >= 0.5) {
				blur.blurAmount += 0.05f;
				timerBlur = 0;
			}
		} else if (blur.blurAmount >= 0.65){
			if (timerBlur >= 0.5) {
				blur.blurAmount -= 0.05f;
				timerBlur = 0;
			}
		}

		//CHROMA ABERRATION (DIMINUI)
		timerChroma += Time.deltaTime;
		if (vig.chromaticAberration > 0) {
			if (timerChroma >= 0.5) {
				vig.chromaticAberration--;
				timerChroma = 0;
			}
		}

		//VELOCIDADE DO PLAYER - MELHORA
		timerWalkSpeed += Time.deltaTime;
		if (timerWalkSpeed >= 0.5) {
			if (moveController.moveSpeed < 4) {
				moveController.moveSpeed++;
				if (moveController.step_interval > 0.7) {
					moveController.step_interval -= 0.1f;
				}
				timerWalkSpeed = 0;
			}	
		}

		timerRunSpeed += Time.deltaTime;
		if (timerRunSpeed >= 0.5) {
			if (moveController.runSpeed < 10) {
				moveController.runSpeed++;
				timerRunSpeed = 0;
			}	
		}

	}

	void Percent75(){
		//SPAWN MONSTRO
		spawnScript.rateSpawn = 10;

		//SOM-VOZES (AUMENTA)
		timerAudioWhispers += Time.deltaTime;
		if (audioS.volume < 0.75) {
			if (timerAudioWhispers >= 0.25) {
				audioS.volume += 0.05f;
				timerAudioWhispers = 0;
			}
		}

		//BLUR (AUMENTA)
		timerBlur += Time.deltaTime;
		if (blur.blurAmount < 0.92) {
			if (timerBlur >= 0.5) {
				blur.blurAmount += 0.05f;
				timerBlur = 0;
			}
		}

		//CHROMA ABERRATIO (AUMENTA)
		timerChroma += Time.deltaTime;
		if (vig.chromaticAberration < 15) {
			if (timerChroma >= 0.5) {
				vig.chromaticAberration++;
				timerChroma = 0;
			}
		}

		//VELOCIDADE DO PLAYER - PIORA
		timerWalkSpeed += Time.deltaTime;
		if (timerWalkSpeed >= 0.5) {
			if (moveController.moveSpeed > 2) {
				moveController.moveSpeed--;
				if (moveController.step_interval < 1) {
					moveController.step_interval += 0.1f;
				}
				timerWalkSpeed = 0;
			}	
		}

		timerRunSpeed += Time.deltaTime;
		if (timerRunSpeed >= 0.5) {
			if (moveController.runSpeed > 4) {
				moveController.runSpeed--;
				timerRunSpeed = 0;
			}	
		}


	}

	void Percent100(){
		//SPAWN MONSTRO
		//spawnScript.rateSpawn = 2;
		timerDelayMorte += Time.deltaTime;
		if (contadorSom < 1) {
			audioS.clip = somMorte;
			audioS.PlayOneShot (somMorte, 1);
			contadorSom++;
		}
		if (timerDelayMorte >= 0.2) {
			estaMorto = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			menuMorte.SetActive (true);
			timerDelayMorte = 0;
			Time.timeScale = 0;
		}


	}

}
