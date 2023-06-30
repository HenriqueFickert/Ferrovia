using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutMenu : MonoBehaviour {

	void Start () {
		
	}
	

	void Update () {
		
	}

	public void GoToMenuAnim(){
		Time.timeScale = 1;
		SceneManager.LoadScene("Menu");
	}

	public void ReloadSceneAnim(){
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
