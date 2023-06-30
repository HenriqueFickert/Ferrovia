using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {


	public GameObject loadingScene;
	public GameObject canvasDesative;
	public GameObject informacoes;
	public Slider slider;
	public Text progressText;
	private bool comecaCarregamento;
	public float tempoAnim;
	public GameObject botoes;
	public GameObject logo;


	public void loadLevel(int sceneIndex)
	{
		//StartCoroutine (LoadAsynchronously(sceneIndex));
		progressText.enabled = false;
		StartCoroutine (preparaCena(sceneIndex));
		loadingScene.SetActive (true);
	}


	IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex+1);

		while (!operation.isDone) {


			float progress = Mathf.Clamp01 (operation.progress / .9f);
			progressText.text =(int)(progress * 100f) + "%";
			slider.value = progress;
			yield return null;
		}
	}


	IEnumerator preparaCena(int sceneIndex){
		botoes.SetActive (false);
		yield return new WaitForSeconds (tempoAnim + 0.5f);
		canvasDesative.SetActive (false);
		informacoes.SetActive (true);
		yield return new WaitForSeconds (tempoAnim + 1.5f);
		progressText.enabled = true;
		StartCoroutine (LoadAsynchronously(sceneIndex));

	}



}
