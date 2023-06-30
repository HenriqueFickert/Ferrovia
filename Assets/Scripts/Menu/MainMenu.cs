using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour {

	public Dropdown resolutionDropDown;
	public AudioMixer mainMixer;

	public Slider mouseSS;
	public Slider brilhoS;

	Resolution[] resolutions;

	public Light luzB1;
	public Light luzB2;

	public AudioSource audioMenuPause;
	public AudioClip clickSound;
	public AudioClip LightButtomSound;

	private bool isAudioLightRunning;

	void Start(){
		AtributosMenu.mouseSensibility = 100f;

		Time.timeScale = 1;

		brilhoS.value = AtributosMenu.brilho;
		luzB1.intensity = AtributosMenu.brilho;
		luzB2.intensity = AtributosMenu.brilho;
		mouseSS.value = AtributosMenu.mouseSensibility;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = enabled;
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



	public void QuitGame(){
		Application.Quit ();
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

	public void Setbrightness(float brightness){
		AtributosMenu.brilho = brightness;
		luzB1.intensity = AtributosMenu.brilho;
		luzB2.intensity = AtributosMenu.brilho;
	}
	public void MouseSensibility(float sensibility){
		//moveController.mouseSensibility = sensibility;
		AtributosMenu.mouseSensibility = sensibility;

	}

	public void StartGame(){
		StartCoroutine (AudioClick ());
		SceneManager.LoadScene ("Jogo");
	}

	IEnumerator AudioClick(){
		audioMenuPause.PlayOneShot (clickSound, 1);
		yield return new WaitForSeconds (clickSound.length);
	}

	public void PlayAudio(){
		audioMenuPause.PlayOneShot (clickSound, 1);
	}



	public void PlayLightAudio(){
		if (!isAudioLightRunning) {
			isAudioLightRunning = true;
			StartCoroutine (AudioLight ());
		}
	}

	IEnumerator AudioLight(){
		audioMenuPause.PlayOneShot (LightButtomSound, 1);
		yield return new WaitForSeconds (LightButtomSound.length);
		isAudioLightRunning = false;
	}

}
