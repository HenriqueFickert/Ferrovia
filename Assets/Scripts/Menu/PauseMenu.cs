using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

	public GameObject tutorial;

	public Dropdown resolutionDropDown;
	public AudioMixer mainMixer;
	Resolution[] resolutions;

	public static bool isPaused;
	public GameObject inventario;
	public GameObject menuPouse;
	public GameObject menuOpcoes;
	public GameObject canvasNormal;
	public GameObject canvasIventario;

	private Insanidade insanidadeScript;

	public Slider mouseSS;
	public Slider brilhoS;

	public Light luzB1;
	public Light luzB2;

	public AudioSource audioMenuPause;
	public AudioClip clickSound;
	public AudioClip LightButtomSound;

	private bool limitePause;

	public GameObject fadeOutJogarNovamente;
	public GameObject fadeOutMenu;

	void Start () {
		AtributosMenu.mouseSensibility = 100f;
		insanidadeScript = FindObjectOfType<Insanidade> ();
		limitePause = true;

		brilhoS.value = AtributosMenu.brilho;
		luzB1.intensity = AtributosMenu.brilho;
		luzB2.intensity = AtributosMenu.brilho;
		mouseSS.value = AtributosMenu.mouseSensibility;

		isPaused = false;

		resolutions = Screen.resolutions;
		resolutionDropDown.ClearOptions ();
		int currentResoltionIndex = 0;
		List <string> options = new List <string> ();
		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);
			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentResoltionIndex = i;
			}
		}

		resolutionDropDown.AddOptions (options);
		resolutionDropDown.value = currentResoltionIndex;
		resolutionDropDown.RefreshShownValue ();
	}



	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			isPaused = !isPaused;
		}

		if (isPaused) {
			if (limitePause) {
				Pouse ();
				limitePause = false;
			}
				
			
		} else if (!isPaused && !inventario.activeSelf && !insanidadeScript.estaMorto){
			if (!limitePause) {
				Resume ();
				limitePause = true;
			}

		}
	}


	public void Resume(){
		isPaused = false;
		menuPouse.SetActive (false);
		menuOpcoes.SetActive (false);
		canvasNormal.SetActive (true);
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
	}

	void Pouse(){
		Time.timeScale = 0;
		canvasNormal.SetActive (false);
		canvasIventario.SetActive (false);
		menuPouse.SetActive (true);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
		

	public void SetQuality( int qualityIndex)
	{
		QualitySettings.SetQualityLevel (qualityIndex);
	}

	public void SetFullScreen(bool fullScreen){
		Screen.fullScreen = fullScreen;
	}

	public void SetResolution(int resolutionIndex){
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetVolumeMusic(float volume){
		mainMixer.SetFloat ("musicVolume", volume);
	}
	public void SetVolumeEffect(float volume){
		mainMixer.SetFloat ("effectVolume", volume);
	}

	public void MouseSensibility(float sensibility){
		AtributosMenu.mouseSensibility = sensibility;

	}

	public void Setbrightness(float brightness){
		AtributosMenu.brilho = brightness;
		luzB1.intensity = AtributosMenu.brilho;
		luzB2.intensity = AtributosMenu.brilho;
	}


	public void GoToMainMenu(){
		StartCoroutine (LoadMenu ());
		tutorial.SetActive (true);
		tutorial.GetComponent<Tutorial> ().ReiniciaTutorial();
		fadeOutMenu.SetActive (true);
		//Time.timeScale = 1;
		//SceneManager.LoadScene("Menu");
	}
		
	IEnumerator LoadMenu(){
		audioMenuPause.PlayOneShot (clickSound, 1);
		yield return new WaitForSeconds (clickSound.length);
	}

	public void PlayAudio(){
		audioMenuPause.PlayOneShot (clickSound, 1);
	}


	public void PlayLightAudio(){
			StartCoroutine (AudioLight ());
	}

	IEnumerator AudioLight(){
		audioMenuPause.PlayOneShot (LightButtomSound, 1);
		yield return new WaitForSeconds (LightButtomSound.length);
	
	}
		
	public void ReloadScene(){
		StartCoroutine (LoadGame ());
		tutorial.SetActive (true);
		fadeOutJogarNovamente.SetActive (true);
		//Time.timeScale = 1;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	IEnumerator LoadGame(){
		audioMenuPause.PlayOneShot (clickSound, 1);
		yield return new WaitForSeconds (clickSound.length);
	}

}
