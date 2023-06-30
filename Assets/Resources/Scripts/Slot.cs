using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slot : MonoBehaviour
{
	[HideInInspector]public int indexSlot;



	private Image ima;
	private Text tx;
	//private Inventory inv;
	public Inventory inv;
	private UIControl uiCon;
	private PlayerCollectable pc;
	private  DataBase db;
  
	void Awake ()
    {
		
		uiCon = FindObjectOfType<UIControl>();
		pc = FindObjectOfType<PlayerCollectable>();
		db = (DataBase)FindObjectOfType(typeof(DataBase));

		ima = transform.GetChild(0).GetComponent<Image>();
		tx = transform.GetChild(1).GetComponent<Text>();
	}
	void Update ()
	{
		

			if (inv.inventory [indexSlot].names != null) {
				ima.enabled = true;
				ima.sprite = inv.inventory [indexSlot].icons;
			} else {
				ima.enabled = false;
			}

			if (inv.inventory [indexSlot].type == TypeItem.COSUMABLE) {
				if (inv.inventory [indexSlot].amount > 0) {
					tx.enabled = true;
					tx.text = inv.inventory [indexSlot].amount.ToString ();
				}
			} else {
				tx.enabled = false;
			}
			if (Input.GetMouseButtonDown (1)) {
			
				if (uiCon.mouseSlot.activeSelf && !Inventory.isItem) {
					pc.DropItens (Inventory.item);
					DesableMouseSlot ();
				}
			}
	}

	public void GetItemSlot()
	{

		if (Input.GetMouseButton (1)) {

			if (inv.inventory [indexSlot].names != null) {
				Inventory.ima = uiCon.mouseSlot.GetComponent<Image> ();
				Inventory.item = inv.inventory [indexSlot];
				Inventory.index = indexSlot;

				if (Inventory.ima.sprite == null) {
					uiCon.mouseSlot.SetActive (true);
					Inventory.ima.sprite = inv.inventory [indexSlot].icons;
					inv.inventory [indexSlot] = new Itens ();

				}
	
			}
		}
	}

	public void DesableMouseSlot()
	{
		Inventory.ima.sprite = null;
		Inventory.item = new Itens();
		uiCon.mouseSlot.SetActive(false);
	}

	public void SetItemSlot()
	{
		if (Input.GetMouseButtonDown (1)) {	


			if (inv.inventory [indexSlot].names == null) {
				if (uiCon.mouseSlot.activeSelf) {
					inv.inventory [indexSlot] = Inventory.item;
					DesableMouseSlot ();
				}
			} else if (inv.inventory [indexSlot].names != null) {
				if (uiCon.mouseSlot.activeSelf) {
					inv.inventory [Inventory.index] = inv.inventory [indexSlot];
					inv.inventory [indexSlot] = Inventory.item;
					DesableMouseSlot ();
				}
			}
		}
	}

	public void GetDcItemSlot()
	{
		Inventory.isItem = true;
		Itens item;
			

		if (inv.inventory[indexSlot].names != null && !uiCon.mouseSlot.activeSelf) 
		{
			item = inv.inventory [indexSlot];
			uiCon.showDcItem.SetActive (Inventory.isItem);

			if (uiCon.showDcItem.activeSelf) 
			{
				uiCon.GetDcItens (item.names, item.attributes);
			}
		}

		else if (inv.inventory[indexSlot].names == null && !uiCon.mouseSlot.activeSelf) 
		{
			Inventory.isItem = true;
			uiCon.showDcItem.SetActive (Inventory.isItem);
			uiCon.GetDcItens ("", "");
		}
	}

	public void DesablePanelDc()
	{
		Inventory.isItem = false;
	}



	public void UseItensOnClik(){

		if (Input.GetMouseButtonDown (0)) {		

			if (inv.inventory [indexSlot].type == TypeItem.COSUMABLE) {
				db.UsoItens (inv.inventory [indexSlot].ID);
				inv.inventory [indexSlot].amount--; 

				if (inv.inventory [indexSlot].amount < 1) {
					pc.AddItemList (inv.inventory [indexSlot].ID);
					//Debug.Log (inv.inventory [indexSlot].names);
					inv.inventory[indexSlot] = new Itens();	
					//pc.AddItemList ();
				}

			}else if(inv.inventory [indexSlot].type == TypeItem.COSUMABLEUNICO){
				db.UsoItens (inv.inventory [indexSlot].ID);
				inv.inventory[indexSlot] = new Itens();

			}else if(inv.inventory [indexSlot].type == TypeItem.INFORMATION){
				uiCon.UsoCartas (inv.inventory [indexSlot].ID);

			}
		}
	}

	/*public void UseItens (string _Name){

		if (inv.inventory [indexSlot].names == _Name) {
			inv.inventory[indexSlot] = new Itens();
			return true;
		}
	}*/





}




