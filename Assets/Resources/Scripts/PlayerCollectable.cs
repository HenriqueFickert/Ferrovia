using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerCollectable : MonoBehaviour
{
	//notificacao de itens novos

	[HideInInspector]public GameObject OBJinvCartas, OBJinvItem, OBJInventario;
	public static int qtdNot, cartasNot, itensNot;
	//private int cartasNot, itensNot;
	public AudioSource somItens;
	public AudioClip coletou, dropou;

	public GameObject Not, invCarNot, invItenNot;
	public Text textNot, infoText, textNotCart, textNotInv;
	   
	public Inventory invItens, invCartas;
    private DataBase db;

	private UIControl uiCon;


	public Transform LocalDrop;

	public float disRay = 2.5f;
	public KeyCode TeclaColetar = KeyCode.E;
    public LayerMask layerMask;

	public List<Itens> listItens = new List<Itens>();

	void Awake(){
		qtdNot = 0;
		cartasNot = 0;
		itensNot = 0;
		OBJinvCartas = GameObject.Find("invCartas");
		OBJinvItem = GameObject.Find("invItens");
		OBJInventario = GameObject.Find("invCanvas");
		uiCon = FindObjectOfType<UIControl>();
		db = FindObjectOfType<DataBase>();
	}


    void Start()
	{
        for (int i = 0; i < db.item.Count; i++)
        {
            listItens.Add(db.item[i]);


            for (int j = 0; j < invItens.inventory.Count; j++)
			{
                if (db.item[i].type == TypeItem.COSUMABLE)
                {
                    if (db.item[i] == invItens.inventory[j])
                    {
                        listItens.Remove(db.item[i]);

                    }
                }
            }

			for (int k = 0; k < invCartas.inventory.Count; k++)
			{
				if (db.item[i].type == TypeItem.COSUMABLE)
				{
					if (db.item[i] == invCartas.inventory[k])
					{
						listItens.Remove(db.item[i]);

					}
				}
			}


        }
	

    }

    void Update(){
        DetectItem();

		textNot.text = qtdNot.ToString ();
		textNotCart.text = cartasNot.ToString ();
		textNotInv.text = itensNot.ToString ();

		if(OBJInventario.activeSelf && OBJinvItem.activeSelf){
			itensNot = 0;
		}
		if(OBJInventario.activeSelf && OBJinvCartas.activeSelf){
			cartasNot = 0;
		}
		if (itensNot == 0 && cartasNot == 0) {
			qtdNot = 0;
		}
		if (qtdNot > 0) {
			Not.SetActive (true);
		} else if(qtdNot <=0 ) {
			Not.SetActive (false);
		}
		if (cartasNot > 0) {			
			invCarNot.SetActive (true);
			textNotCart.enabled = true;
		} else if(cartasNot <=0 ) {
			invCarNot.SetActive (false);
			textNotCart.enabled = false;
		}
		if (itensNot > 0) {			
			invItenNot.SetActive (true);
			textNotInv.enabled = true;
		} else if(itensNot <=0 ) {
			invItenNot.SetActive (false);
			textNotInv.enabled = false;
		}
    }

    public void DropItens(Itens item)
    {
		GameObject tmpDrop = (GameObject)Instantiate(item.itemDrop, LocalDrop.position, Quaternion.identity);
        Collectable tmpClt = tmpDrop.GetComponent<Collectable>();
		tmpClt.gameObject.name = item.names;
		tmpClt.nameItem = item.names;
        tmpClt.amount = item.amount;
		tmpClt.ID = item.ID; 
		somItens.PlayOneShot (dropou);

		uiCon.GetDcItens ("", "");

        if (item.type == TypeItem.COSUMABLE)
        {
            listItens.Add(item);
        }

    }

    private void DetectItem()
    {
       
		Ray ray = Camera.main.ScreenPointToRay(new Vector3( Screen.width/2, Screen.height/2, 0));
		RaycastHit hit;


		if (Physics.Raycast (ray, out hit, disRay, layerMask) ) {

			if(hit.collider.tag != "Untagged" && hit.collider.tag != "Portas"){
			Collectable collectable = hit.collider.GetComponent<Collectable> ();

			if (Input.GetKeyDown (TeclaColetar) && collectable.ID <= 100 ) {
				somItens.PlayOneShot (coletou);
				CollectableItens (collectable);
			}else if (Input.GetKeyDown (TeclaColetar) && collectable.ID > 100) {
				somItens.PlayOneShot (coletou);
				CollectableCartas (collectable);
			}
		  }
		}

    }

    private void CollectableItens(Collectable collectable){		
		infoText.text = " ";
		itensNot++; 
		qtdNot++;
        foreach (Itens i in invItens.inventory){
			
            if (collectable.nameItem == i.names){
				if (i.type == TypeItem.COSUMABLE) {
					i.amount += collectable.amount;
					Destroy (collectable.gameObject);
				} 
            }
        }
        for (int i = 0; i < listItens.Count; i++){
			
			if (collectable.nameItem == listItens[i].names){
				
				invItens.AddItens(collectable.nameItem, collectable.amount);
                Destroy(collectable.gameObject);

				if (listItens[i].type == TypeItem.COSUMABLE ){					
					listItens.Remove(listItens[i]);
                }
                break;
			}
		}
    }

	private void CollectableCartas(Collectable collectable){
		infoText.text = " ";
		cartasNot++;
		qtdNot++;
		foreach (Itens i in invCartas.inventory){
			
			if (collectable.nameItem == i.names){
				
				if (i.type == TypeItem.COSUMABLE) {
					i.amount += collectable.amount;
					Destroy (collectable.gameObject);
				} 
			}
		}
		for (int i = 0; i < listItens.Count; i++){
			if (collectable.nameItem != listItens[i].names){
				invCartas.AddItens(collectable.nameItem, collectable.amount);
				Destroy(collectable.gameObject);

				if (listItens[i].type == TypeItem.COSUMABLE ){
					listItens.Remove(listItens[i]);
				}
				break;
			}
		}
	}

	public void AddItemList(int _ID){

		if(_ID == 2){
			listItens.Add(new Itens("Remédios","Esses analgésicos poderão me ajudar a manter o controle sobre o que eu vejo.",TypeItem.COSUMABLE, 2));
		}
	}
}
