using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	[HideInInspector]public static bool usouPrimeiraBateria, fezTudo;
	public PortaComum porta;
	public GameObject primeiraBateria, primeiraChave, primeiraCarta, pCarta;
	private GameObject /* player,*/ invCanvas, invItens;
	public Text cxTexto, cxName, cxTutorial;
	private bool acendeuUmaVez, pegouBateria, pegouCarta, pegouChave, abriuInv;
	private int pegouTudo = 0;
	private BoxCollider box;

	private bool saiuPier = false;

	public GameObject tutorial;

	void Awake(){
		//player = GameObject.FindWithTag ("Player");
		invCanvas = GameObject.Find("invCanvas");
		invItens = GameObject.Find ("invItens");
		box = gameObject.GetComponent<BoxCollider> ();
	}

	void Start () {
		MoveController.movementOn = false;
		cxName.text = "";
		cxTexto.text = "";
		cxTutorial.text = "Pressione a tecla 'F' para ligar a lanterna.";
		AtivaInvent.invOn = false;
	}	

	void Update () {

		if (Input.GetKey (KeyCode.F) && acendeuUmaVez == false) {
			acendeuUmaVez = true;
			MoveController.movementOn = true;
			cxName.text = "";
			cxTexto.text = "";
			cxTutorial.text = "Segure a tecla 'SHIFT' para correr.";
		}

		if (primeiraBateria == null && pegouBateria == false) {
			pegouBateria = true;
			pegouTudo++;		
		}
		if (primeiraChave == null && pegouChave == false) {
			pegouChave = true;
			pegouTudo++;
		}
		if (primeiraCarta == null && pegouCarta == false) {
			pegouCarta = true;
			pegouTudo++;
		}

		if (pegouTudo == 3 && !invCanvas.activeSelf) {			
			box.enabled = false;
			cxName.text = "";
			cxTexto.text = "";
			cxTutorial.text = "Pressione a tecla 'Q' para abrir seu inventário.";		
			if (abriuInv == false) {
				AtivaInvent.invOn = true;
			}
		}

		if (Input.GetKey (KeyCode.Q) && invCanvas.activeSelf && invItens.activeSelf && !fezTudo) {
			abriuInv = true;
			cxName.text = "";
			cxTexto.text = "";
			cxTutorial.text = "Clique na aba Cartas.";
		}

		if (invCanvas.activeSelf && !invItens.activeSelf && !fezTudo) {
			cxName.text = "";
			cxTexto.text = "";
			cxTutorial.text = "Pressione o botão esquerdo do mouse em cima da carta para lê-la.";
		}

		if(pCarta.activeSelf){
			fezTudo = true;
		}

		if (invCanvas.activeSelf && fezTudo) {
			cxName.text = "";
			cxTexto.text = "";
			cxTutorial.text = "Volte para aba Mochila e clique em cima da bateria para carregar a lanterna.";
		}
		if(usouPrimeiraBateria == true && saiuPier == false){
			cxName.enabled = true;
			cxTexto.enabled = true;
			cxName.text = "";
			cxTexto.text = "";
			cxTutorial.text = "Feche o inventário pressionando a tecla 'Q'. Pronto! Agora você pode sair da casa do pier.";
			porta.estaTrancada = false;
		}

		if(porta.estaAberta == true){
			saiuPier = true;
		}

		if (saiuPier == true) {

			cxName.text = "";
			cxTexto.text = "";
			cxTutorial.text = "";
			ReiniciaTutorial ();
		}
	}
	void OnTriggerStay(Collider colisor){
		if (colisor.gameObject.CompareTag ("Player")) {
			cxTutorial.text = "Colete todos os Itens espalhados pela casa do pier.";

		}
	}

	public void ReiniciaTutorial(){
		
		acendeuUmaVez = false;
		pegouBateria = false;
		pegouCarta = false;
		pegouChave = false;
		abriuInv = false;
		usouPrimeiraBateria = false;
		fezTudo = false;
		tutorial.SetActive (false);

	}


}
