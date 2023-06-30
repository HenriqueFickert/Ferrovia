using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField] MoveController moveController;
	[SerializeField] AtributosPlayer attribute;
	[SerializeField] Lanterna lanterna;
	[SerializeField] TurnOnLantern turnOnScript;
	[SerializeField] Slider estaminaBar;
	[SerializeField] Image fillSlider;
	private Color corFill;
	private float timerSlider;

	[SerializeField] GameObject [] batteries;
	[SerializeField] Image hudLanterna;
	[SerializeField] Sprite lanternaligada;
	[SerializeField] Sprite lanternaDesligada;
	private float timerBatteries;

	void Start(){
		corFill = fillSlider.color;
		corFill.a = 0f;
		fillSlider.color = corFill; 
	}

	void Update () {
		if (!moveController.isRunning && moveController.runTime >= 10) {
			timerSlider += Time.deltaTime;
		}
		//ESTAMINA
		if (moveController.isRunning) {
			timerSlider = 0;
			corFill = fillSlider.color;
			corFill.a = 0.5f;
			fillSlider.color = corFill; 
		} else if (!moveController.isRunning && moveController.runTime < 10) {
			timerSlider = 0;
			corFill = fillSlider.color;
			corFill.a = 0.2f;
			fillSlider.color = corFill; 
		} else if (timerSlider >= 2){
			corFill = fillSlider.color;
			corFill.a = 0f;
			fillSlider.color = corFill; 
		}

		estaminaBar.value = moveController.runTime;

		//LANTERNA

		if (turnOnScript.isOn) {
			hudLanterna.sprite = lanternaligada; 
		} else {
			hudLanterna.sprite = lanternaDesligada; 
		}



		if (lanterna.is100Percent) {
			if (turnOnScript.isOn) {
				timerBatteries += Time.deltaTime;
				if (timerBatteries >= 0.5) {
					batteries [0].SetActive (false);
				}
				if (timerBatteries >= 1) {
					batteries [0].SetActive (true);
					timerBatteries = 0;
				}
			} else {
				batteries [0].SetActive (true);
			}
			batteries [1].SetActive (true);
			batteries [2].SetActive (true);
			batteries [3].SetActive (true);
		} else if (lanterna.is75Percent) {
			batteries [0].SetActive (false);


			if (turnOnScript.isOn) {
				timerBatteries += Time.deltaTime;
				if (timerBatteries >= 0.5) {
					batteries [1].SetActive (false);
				}
				if (timerBatteries >= 1) {
					batteries [1].SetActive (true);
					timerBatteries = 0;
				}
			} else {
				batteries [1].SetActive (true);
			}

			batteries [2].SetActive (true);
			batteries [3].SetActive (true);
		} else if (lanterna.is50Percent) {
			batteries [0].SetActive (false);
			batteries [1].SetActive (false);
			if (turnOnScript.isOn) {
				timerBatteries += Time.deltaTime;
				if (timerBatteries >= 0.5) {
					batteries [2].SetActive (false);
				}
				if (timerBatteries >= 1) {
					batteries [2].SetActive (true);
					timerBatteries = 0;
				}
			} else {
				batteries [2].SetActive (true);
			}
			batteries [3].SetActive (true);
		}
		else if (lanterna.is25Percent) {
			batteries [0].SetActive (false);
			batteries [1].SetActive (false);
			batteries [2].SetActive (false);
			if (turnOnScript.isOn) {
				timerBatteries += Time.deltaTime;
				if (timerBatteries >= 0.5) {
					batteries [3].SetActive (false);
				}
				if (timerBatteries >= 1) {
					batteries [3].SetActive (true);
					timerBatteries = 0;
				}
			} else {
				batteries [3].SetActive (true);
			}
		}else if (!lanterna.is100Percent && !lanterna.is75Percent && !lanterna.is50Percent && !lanterna.is25Percent) {
			batteries [0].SetActive (false);
			batteries [1].SetActive (false);
			batteries [2].SetActive (false);
			batteries [3].SetActive (false);
		}
	}
}
