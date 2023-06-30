using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataBase : MonoBehaviour
{
	GameObject player;
	AtributosPlayer attribute;
	TurnOnLantern turnOnScript;
	Lanterna lanterna;
	private bool primeiraBateria = false;
	public AudioSource somItens;
	public AudioClip usandoBateria, usandoRemedios;

	public List<Itens> item = new List<Itens>();

	void Awake ()
	{

		item.Add(new Itens("Bateria", "Poderei recarregar minha lanterna com esta bateria.",TypeItem.COSUMABLEUNICO,0));
		item.Add(new Itens("Chave do Escritório", "Essa chave deve abrir a porta do escritório.",TypeItem.EQUIPAMENT,1));
		item.Add(new Itens("Remédios","Esses analgésicos poderão me ajudar a manter o controle sobre o que eu vejo.",TypeItem.COSUMABLE, 2));
		item.Add(new Itens("Chave do Galpão", "Essa chave deve abrir algum dos galpões.",TypeItem.EQUIPAMENT,3));
		item.Add(new Itens("Colar de Ossos do Pai", "Eu não acredito no que estou vendo",TypeItem.EQUIPAMENT,4));
		item.Add(new Itens("Colar de Ossos do Vô", "Estou preocupado",TypeItem.EQUIPAMENT,5));
		item.Add(new Itens("Colar de Ossos do Bisavô", "Isso é terrivel",TypeItem.EQUIPAMENT,6));

		item.Add(new Itens("Carta", "Pode ser um documento deixado por meu pai.",TypeItem.INFORMATION,101));
		item.Add(new Itens("Pedaço do Diário", "Parece ser uma folha rasgada do diário de meu pai. (Pedaço número 1).",TypeItem.INFORMATION,102));
		item.Add(new Itens("Pedaço do Diário 2", "Outra folha rasgada do diário de meu pai. (Pedaço número 2).",TypeItem.INFORMATION,105));
		item.Add(new Itens("Documento Histórico(1)", "Hun um documento antigo",TypeItem.INFORMATION,106));
		item.Add(new Itens("Documento Histórico(2)", "Alguém deixou muito desses aqui",TypeItem.INFORMATION,107));
		item.Add(new Itens("Documento Histórico(3)", "Será que esse explica o que aconteceu aqui?",TypeItem.INFORMATION,108));

	}


	void Start(){
		player = GameObject.FindWithTag ("Player");
		lanterna = player.GetComponentInChildren<Lanterna> ();
		attribute =  player.GetComponent<AtributosPlayer> ();
		turnOnScript =  player.GetComponent<TurnOnLantern> ();
	}

	public void UsoItens(int _ID){

		if (_ID == 0) {
			attribute.currentBatery = 180;
			lanterna.recarregando = true;
			turnOnScript.isOn = true;
			somItens.PlayOneShot (usandoBateria);

			if (primeiraBateria == false) {
				primeiraBateria = true;
				Tutorial.usouPrimeiraBateria = true;
			}				
		}

		if (_ID == 2) {

			attribute.insanity -= 8;
			somItens.PlayOneShot (usandoRemedios);

		}

	}


}
