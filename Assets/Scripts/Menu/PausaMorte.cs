using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausaMorte : MonoBehaviour {

	public Button button1;
	public Button button2;


	public void DesativaButtons(){
		button1.interactable = false;
		button2.interactable = false;
	}
	public void StopGame()
	{
		button1.interactable = true;
		button2.interactable = true;
		Time.timeScale = 1;
	}
}
