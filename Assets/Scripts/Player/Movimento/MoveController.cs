using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

	private Insanidade insandadeScript;

	private CharacterController _controller;
	[HideInInspector] public static bool movementOn = true;

	//Rotação do Player
	//public float mouseSensibility = 100.0f;
	private Vector3 _mouseX;
	public GameObject head;
	float _mouseY;

	//Movimentação
	Vector3 _movement ;
	public float moveSpeed = 4.0f;
	public float runSpeed = 10.0f;


	//Corrida
	public AudioSource audioEstamina;
	public AudioClip somEstamina;
	private int contadorSom;
	[HideInInspector]	public bool isRunning;
	public float runTime = 10;
	private float delayRunTime;
	private bool isInDelay;

	//Gravidade
	[SerializeField] float gravityMultiplier = 2.0f;

	//Pulo
	//[SerializeField] private float jumpForce = 75.0f;
	//[SerializeField] private float jumpTime = 0.5f;
	//private bool isJumping;
	//private float timeJumpDelay;
	//private float auxJumpTime;

	//Som de Passo
	public float step_interval = 0.3f;
	private float aux_interval = 0.0f;
	public int isOnGround;

	//Camera
	public bool isTheCameraActive;

	//	-------------------------------------------------------------- VOID START -------------------------------------------------------------- 
	void Start () {
		insandadeScript = GetComponent<Insanidade> ();
		_controller = GetComponent <CharacterController> ();
	}
	//	-------------------------------------------------------------- VOID UPDATE --------------------------------------------------------------
	void Update () {
		//Rotação - Usando a do Professor
		//PlayerRotate ();
		_mouseX = new Vector3 (0,Input.GetAxis("Mouse X"),0);
		_mouseY -= Input.GetAxis ("Mouse Y")   * AtributosMenu.mouseSensibility  ;
		_mouseY = Mathf.Clamp (_mouseY, -2000 , 2000 );

		//Movimentação de corrida por tempo

		if (movementOn) {
			if (!PauseMenu.isPaused && !insandadeScript.estaMorto && Input.GetButton ("Run") && (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical"))) {
				RunPerTime ();
			} else {
				isRunning = false;
				delayRunTime += Time.deltaTime;
				if (delayRunTime >= 1) {
					if (runTime < 10) {
						runTime++;
						delayRunTime = 0;
					}
				}
			}
			_movement = GetKeys () * GetSpeed (); // o vector 3 movimento é a multiplicação do return dos dois metodos

			//Gravidade
		
			//Pulo
			//_movement.y = Jump (_movement.y);

			//Fim Movimento
			_movement = transform.TransformDirection (_movement);

			//Limintar a velocidade angular
			if (isRunning) {
				_movement.x = Mathf.Clamp (_movement.x, -10, 10);
				_movement.z = Mathf.Clamp (_movement.z, -10, 10);
			} else {
				_movement.x = Mathf.Clamp (_movement.x, -4, 4);
				_movement.z = Mathf.Clamp (_movement.z, -4, 4);
			} 

			if (isRunning && Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
				_movement.x = Mathf.Clamp (_movement.x, -7, 7);
				_movement.z = Mathf.Clamp (_movement.z, -7, 7);
			} else if (!isRunning && Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) {
				_movement.x = Mathf.Clamp (_movement.x, -3.5f, 3.5f);
				_movement.z = Mathf.Clamp (_movement.z, -3.5f, 3.5f);
			} 

			if (!isTheCameraActive && !insandadeScript.estaMorto) {
				//Som Passo
				if (_movement.x != 0 || _movement.z != 0) {
					PlayAudio ();
				}
				//SomEstamina
				if (isInDelay) {
					if (contadorSom < 1) {
						audioEstamina.clip = somEstamina;
						audioEstamina.PlayOneShot (somEstamina, 1);
						contadorSom++;
					}
				} else {
					contadorSom = 0;
				}
			}
	
		} else if (movementOn == false) {
			_movement.x = Mathf.Clamp (_movement.x, -0, 0);
			_movement.z = Mathf.Clamp (_movement.z, -0, 0);

		}
		//print (_mouseY);
	//	print (head.transform.localRotation);
	}
		
	void FixedUpdate(){
		if (!isTheCameraActive && !insandadeScript.estaMorto) {
			_movement.y += Physics.gravity.y * gravityMultiplier;
			//Aplica o movimento
			_controller.Move (_movement * Time.fixedDeltaTime);

			//Rotação X
			transform.Rotate (_mouseX * Time.fixedDeltaTime* AtributosMenu.mouseSensibility);
			//Rotação Y
			head.transform.localRotation = Quaternion.Euler (_mouseY * Time.fixedDeltaTime, 0, 0);
		}
	}

	//	-------------------------------------------------------------- METODOS --------------------------------------------------------------

	//Pega os Axis e coloca em um Vector 3
	Vector3 GetKeys(){
		
			float lh = Input.GetAxis ("Horizontal");
			float lv = Input.GetAxis ("Vertical");

			return  new Vector3 (lh, 0, lv);
	}
		
	//Verifica se ta correndo ou andando
	float GetSpeed(){
		if (isRunning) {
			return runSpeed;
		} else
			return moveSpeed;
	}
		
	//Corrida do personagem beaseado na estamina (runTime)
	void RunPerTime(){
		if (runTime > 0 && !isInDelay) {
			isRunning = true;
			delayRunTime += Time.deltaTime;
			if (delayRunTime >= 1) {
				runTime--;
				delayRunTime = 0;
			}
		} else {
			isInDelay = true;
			isRunning = false;
			delayRunTime += Time.deltaTime;
			if (delayRunTime >= 1) {
				if (runTime < 10) {
					runTime++;
					delayRunTime = 0;
				}
			}
			if (runTime >= 5) {
				isInDelay = false;
			}
		}
	}
		
	//Som de Passo
	private void PlayAudio(){
		aux_interval += Time.deltaTime;

		if (isOnGround == 0) { //somGrama
			if ((isRunning && aux_interval >= (step_interval / 2) || (aux_interval >= step_interval)) /*&& !isJumping*/) {
				GetComponent<AudioController> ().PlayHardStep ();
				aux_interval = 0.0f;
			}
		} else if (isOnGround == 1) {

			if ((isRunning && aux_interval >= (step_interval / 2) || (aux_interval >= step_interval)) /*&& !isJumping*/) {
				GetComponent<AudioController> ().PlayGrassStep ();
				aux_interval = 0.0f;
			}

		} else if (isOnGround == 2) {

			if ((isRunning && aux_interval >= (step_interval / 2) || (aux_interval >= step_interval)) /*&& !isJumping*/) {
				GetComponent<AudioController> ().PlayWoodStep ();
				aux_interval = 0.0f;
			}

		} else if (isOnGround == 3) {
			if ((isRunning && aux_interval >= (step_interval / 2) || (aux_interval >= step_interval)) /*&& !isJumping*/) {
				GetComponent<AudioController> ().PlayWaterStep ();
				aux_interval = 0.0f;
			}
		} else if (isOnGround == 4) {
			if ((isRunning && aux_interval >= (step_interval / 2) || (aux_interval >= step_interval)) /*&& !isJumping*/) {
				GetComponent<AudioController> ().PlayGroundStep ();
				aux_interval = 0.0f;
			}
		}

	}
		
	//	-------------------------------------------------------------- COMENTARIOS --------------------------------------------------------------

	//PULO
  /*private float Jump(float y){
		if (_controller.isGrounded && !isJumping) {
			if (Input.GetButton ("Jump") ) {
				isJumping = true;
				auxJumpTime = 0.0f;
			}
		} else {
			if (auxJumpTime >= jumpTime) {
						timeJumpDelay += Time.deltaTime;
						if (timeJumpDelay >= 0.15f){
							isJumping = false;
							timeJumpDelay = 0;
						}

			}
						
				
					
			if (isJumping) {
				auxJumpTime += Time.deltaTime;
				y += (jumpForce * (jumpTime - auxJumpTime));
			}
			y += Physics.gravity.y * gravityMultiplier;
		}
		return y;
	}
*/

	//rotação do personagem no eixo X - video
	/*void PlayerRotate(){
		_mouseX = Input.GetAxis ("Mouse X");
		Vector3 rotacao = transform.localEulerAngles;
		rotacao.y += _mouseX * mouseXSensibility * Time.deltaTime;
		transform.localEulerAngles = rotacao;
	}*/


}
