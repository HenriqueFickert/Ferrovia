using UnityEngine;
using System.Collections;

public enum TypeItem
{
    NULL,
    COSUMABLE,
	COSUMABLEUNICO,
	EQUIPAMENT,
	INFORMATION


}

[System.Serializable]
public class Itens
{
    public string names;
    public string attributes;
    
    public int amount;
	public int ID;
    public Sprite icons;
    public GameObject itemDrop;
    public Sprite iconsNeutral;
    public TypeItem type;

	public Itens(string name, string attributes,TypeItem _type,int _ID)
    {
        this.names = name;
        this.attributes = attributes;      
        this.type = _type;
		this.ID = _ID;
        this.itemDrop = Resources.Load<GameObject>("DropItens/" + names);
        this.icons = Resources.Load<Sprite>("Icons/" + names);
        this.iconsNeutral = Resources.Load<Sprite>("Icons/IconsNeutral/" + names);
    }

    public Itens()
    {
    }

}
