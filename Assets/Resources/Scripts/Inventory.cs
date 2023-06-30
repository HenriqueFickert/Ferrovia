using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<Itens> inventory = new List<Itens>();

    public static Itens item;
    public static Image ima;
    public static int index;
    public static bool isItem;

    private int contChild;
    private  DataBase db;

    void Awake()
    {
        contChild = transform.childCount;
        db = (DataBase)FindObjectOfType(typeof(DataBase));

        for (int i = 0; i < contChild; i++)
        {
            transform.GetChild(i).GetComponent<Slot>().indexSlot = i;
            inventory.Add(new Itens());
            Data.saveItens.Add(new DataItens());
            Data.loadItens.Add(new DataItens());
        }

        //AddItensSlot();
    }    

    public void AddItens(string name, int amount)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].names == null)
            {
                for (int j = 0; j < db.item.Count; j++)
                {
                    if (db.item[j].names == name)
                    {
                        inventory[i] = db.item[j];

                    }
					if (inventory[i].type == TypeItem.COSUMABLE)
                    {
                        inventory[i].amount = amount;
                    }
					else if (inventory[i].type != TypeItem.COSUMABLE)
                    {
                        inventory[i].amount = 1;
                    }
                }
                break;
            }
        }
    }


	public bool procuraSlotComItem (int _ID){

		int Id = _ID;
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory [i].ID == Id ) {
				inventory [i] = new Itens ();
				return true;
			} 
	 }
		return false;
	}


	public void AddItensSlot(string name, int amount)
    {
		PlayerCollectable.qtdNot++;
		PlayerCollectable.itensNot++;
		Debug.Log (PlayerCollectable.qtdNot);
		AddItens (name, amount);
    }


}
