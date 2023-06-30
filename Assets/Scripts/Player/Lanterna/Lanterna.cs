using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanterna : MonoBehaviour {
	//Acesso a Scripts
	AtributosPlayer attribute;
	TurnOnLantern turnOnScript;
	MoveController moveController;

	//Componentes
	Light lantern;

	//Decreasing intesity
	private float timerBatery;
	public bool is100Percent;
	public bool is75Percent;
	public bool is50Percent;
	public bool is25Percent;
	private float timerDecreasing; 

	//Blink Lantern
	private float delayBlinkLightTimer;
	private float blinkLightTimer;
	private int randomBlinkLight;
	private bool isUnder50Percent;
	private bool isBlinking;

	//RecarregaBateria
	public bool recarregando;

	//Tutorial
	public bool estaNoTutorial;

	void Start () {
		lantern = GetComponent<Light> ();
		attribute = GetComponentInParent<AtributosPlayer> ();
		turnOnScript = GetComponentInParent<TurnOnLantern> ();
		moveController = GetComponentInParent <MoveController> ();
		attribute.currentBatery = 89;
		estaNoTutorial = true;
	}
	

	void Update () {
		//Decai a quantidade de bateria
		if (turnOnScript.isOn) {
			if (!estaNoTutorial) {
				if (!moveController.isTheCameraActive) {
					if (attribute.currentBatery > 0) {
						timerBatery += Time.deltaTime;
						if (timerBatery >= 1) {
							attribute.currentBatery--;
							timerBatery = 0;
						}
					}
				}
			}
		}

		//Recarregando
		if(recarregando){
			if (attribute.currentBatery >= 135) {
				lantern.intensity = 4;
			} else if (attribute.currentBatery >= 90 && attribute.currentBatery < 135) { 
				lantern.intensity = 3;
			} else if (attribute.currentBatery >= 45 && attribute.currentBatery < 90) {
				lantern.intensity = 2;
			} else if (attribute.currentBatery > 0 && attribute.currentBatery < 45) {
				lantern.intensity = 1;
			} else if (attribute.currentBatery >= 0) {
				lantern.intensity = 0;
			}

			recarregando = false;
		}

		//Fases da bateria de acordo com o tempo
		if (attribute.currentBatery >= 135) {
			is100Percent = true;
			is75Percent = false;
			is50Percent = false;
			is25Percent = false;
			isUnder50Percent = false;
			lantern.intensity = 4;
		} else if (attribute.currentBatery >= 90 && attribute.currentBatery < 135) { 
			is100Percent = false;
			is75Percent = true;
			is50Percent = false;
			is25Percent = false;
			isUnder50Percent = false;
		} else if (attribute.currentBatery >= 45 && attribute.currentBatery < 90) {
			is100Percent = false;
			is75Percent = false;
			is50Percent = true;
			is25Percent = false;
			isUnder50Percent = false;
		} else if (attribute.currentBatery > 0 && attribute.currentBatery < 45) {
			is100Percent = false;
			is75Percent = false;
			is50Percent = false;
			is25Percent = true;
		} else if (attribute.currentBatery >= 0) {
			is100Percent = false;
			is75Percent = false;
			is50Percent = false;
			is25Percent = false;
			isUnder50Percent = false;
			turnOnScript.isOn = false;
			lantern.intensity = 0;
		}

		//O que acontece em cada fase da bateria
		if (is75Percent) {
			if (lantern.intensity > 3.1) {
				timerDecreasing += Time.deltaTime;
				if (timerDecreasing >= 0.5) {
					for (int i = 0; i < 1; i++) {
						lantern.intensity -= 0.1f;
					}
					timerDecreasing = 0;
				}
			}
		}

		if (is50Percent) {
			if (lantern.intensity > 2.1) {
				timerDecreasing += Time.deltaTime;
				if (timerDecreasing >= 0.5) {
					for (int i = 0; i < 1; i++) {
						lantern.intensity -= 0.1f;
					}
					timerDecreasing = 0;
				}
			}
		}

		if (is25Percent) {
			if (lantern.intensity > 1.1) {
				timerDecreasing += Time.deltaTime;
				if (timerDecreasing >= 0.5) {
					for (int i = 0; i < 1; i++) {
						lantern.intensity -= 0.1f;
					}
					timerDecreasing = 0;
				}
			}
			if (lantern.intensity >= 1 && lantern.intensity < 1.1) {
				isUnder50Percent = true;
			}
		}

	//Blink Lantern
		//Random Blink
		if (isUnder50Percent) {
			delayBlinkLightTimer += Time.deltaTime;
			if (delayBlinkLightTimer >= 3.5) {
				randomBlinkLight = Random.Range (0, 3);
				delayBlinkLightTimer = 0;
			}

			//Tipos de Blink
			if (randomBlinkLight == 0) {
				lantern.intensity = 1;
			} else if (randomBlinkLight == 1) {
				blinkLightTimer += Time.deltaTime;
				if (blinkLightTimer > 0 && blinkLightTimer < 0.1 ) {
					lantern.intensity = 0;
				} else if (blinkLightTimer >= 0.1  && blinkLightTimer < 0.2 ) {
					lantern.intensity = 1;
				}
				else if (blinkLightTimer >= 0.2  && blinkLightTimer < 0.3 ) {
					lantern.intensity = 0;
				}else if (blinkLightTimer >= 0.3  && blinkLightTimer < 0.4 ) {
					lantern.intensity = 1;
				}else if (blinkLightTimer >= 0.4  && blinkLightTimer < 0.5 ) {
					lantern.intensity = 0;
				}else if (blinkLightTimer >= 0.5) {
					lantern.intensity = 1;
					blinkLightTimer = 0;
					randomBlinkLight = 0;
				}
			} else if (randomBlinkLight == 2){
				blinkLightTimer += Time.deltaTime;
				if (blinkLightTimer > 0 && blinkLightTimer < 0.1 ) {
					lantern.intensity = 0;
				} else if (blinkLightTimer >= 0.1  && blinkLightTimer < 0.2 ) {
					lantern.intensity = 1;
				}
				else if (blinkLightTimer >= 0.2  && blinkLightTimer < 0.3 ) {
					lantern.intensity = 0;
				}else if (blinkLightTimer >= 0.3  && blinkLightTimer < 0.4 ) {
					lantern.intensity = 1;
				}else if (blinkLightTimer >= 0.4  && blinkLightTimer < 1 ) {
					lantern.intensity = 0;
				}else if (blinkLightTimer >= 1  && blinkLightTimer < 1.1 ) {
					lantern.intensity = 1;
				}else if (blinkLightTimer >= 1.1  && blinkLightTimer < 1.2 ) {
					lantern.intensity = 0;
				}else if (blinkLightTimer >= 1) {
					lantern.intensity = 1;
					blinkLightTimer = 0;
					randomBlinkLight = 0;
				}
			}
		}
	}

}
