using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIControl : MonoBehaviour
{

	[HideInInspector]public GameObject invItens, invCartas;

	public static bool invEmUso = false;
	private GameObject invCanvas;

	public GameObject mouseSlot;
	public GameObject showDcItem;
	public Text nameItens, attributes, tutorial; //value;

	public GameObject carta;
	private Image imageCarta; 
	private bool cartaAtiva;

	public AudioSource somUI, somCartas;
	private AudioClip somCartaAtiva; 
	public AudioClip somPapel, somTrocaInv;

	void Awake ()
	{		
		invCanvas = GameObject.Find("invCanvas");
		invCartas = GameObject.Find("invCartas");
		invItens = GameObject.Find("invItens");        
		mouseSlot.SetActive(false);

		imageCarta = carta.GetComponent<Image>();
    }
	
	void Update ()
	{
		if (mouseSlot.activeSelf)
		{
			MouseSlot();

        }

		if (showDcItem.activeSelf) 
		{
			ShowDcItem ();
		}

		if(cartaAtiva == true && Input.GetKeyDown(KeyCode.E)){

			//Time.timeScale = 1;
			carta.SetActive (false);
			cartaAtiva = false;
			somUI.PlayOneShot (somPapel);
			AtivaInvent.invOn = true;
			invCanvas.SetActive(true);
			nameItens.enabled = true;
			attributes.enabled = true;
			tutorial.enabled = true;
		}


	} 
	private void MouseSlot()
	{
        mouseSlot.transform.position = Input.mousePosition;
	}

	private void ShowDcItem()
	{
       
	}

	public void GetDcItens(string nameItens,string attributes)
	{
		this.nameItens.text = nameItens;
		this.attributes.text = attributes;        
	}

	public void trocaInv(){

		somUI.PlayOneShot (somTrocaInv);

		if (invEmUso == true) {
			invEmUso = false;
			invItens.SetActive (true);

		} else if (invEmUso == false) {
			
			invEmUso = true;
			invCartas.SetActive (false);

		}

		if (invEmUso == false) {
			
			invItens.gameObject.SetActive(true);
			invCartas.gameObject.SetActive(false);


		}else if (invEmUso == true) {
			invItens.gameObject.SetActive(false);
			invCartas.gameObject.SetActive(true);
		}
	}


	public void UsoCartas(int _ID){

		somUI.PlayOneShot (somPapel);
		nameItens.enabled = false;
		attributes.enabled = false;
		tutorial.enabled = false;
		imageCarta.sprite = Resources.Load<Sprite>("Cartas/" + _ID);
		invCanvas.SetActive(false);
		//Time.timeScale = 0;
		AtivaInvent.invOn = false;
		carta.SetActive (true);
		cartaAtiva = true;
		somCartaAtiva =  Resources.Load<AudioClip>("Sons/" + _ID);
		somCartas.PlayOneShot (somCartaAtiva);
	} 
}
