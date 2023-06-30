using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class Data 
{ 
    static public List<DataItens> saveItens = new List<DataItens>();
    static public  List<DataItens> loadItens = new List<DataItens>();

    static public void Save(List<Itens> itens)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "data");

        for (int i = 0; i < itens.Count; i++)
        {
            if (itens[i].names != null)
            {
                saveItens[i].names = itens[i].names;
                saveItens[i].attributes = itens[i].attributes;
                
                saveItens[i].amount = itens[i].amount;
                saveItens[i].type = (int)itens[i].type;
            }
        }
        bf.Serialize(fs, saveItens);
        fs.Close();
    }

    static public void Load(Inventory inv)
    {
        if (File.Exists(Application.persistentDataPath + "data"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "data",FileMode.Open);
            loadItens = (List<DataItens>)bf.Deserialize(fs);
            fs.Close();

            for (int i = 0; i < inv.inventory.Count; i++)
            {
                inv.inventory[i] = new Itens(); 

                inv.inventory[i].names = loadItens[i].names;
                inv.inventory[i].attributes = loadItens[i].attributes;
                inv.inventory[i].amount = loadItens[i].amount;
                inv.inventory[i].type = (TypeItem)loadItens[i].type;

                inv.inventory[i].itemDrop = Resources.Load<GameObject>("Prefabs/DropItens/" + loadItens[i].names);
                inv.inventory[i].icons = Resources.Load<Sprite>("Icons/" + loadItens[i].names);
                inv.inventory[i].iconsNeutral = Resources.Load<Sprite>("Icons/IconsNeutral/" + loadItens[i].names);
            }
        }
       
    }
	
}
[Serializable]
public class DataItens
{
    public string names;
    public string attributes;
    public int valueBuy;
    public int valueSell;
    public int amount;
    public int type;
}
