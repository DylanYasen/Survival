using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
interface Interactable
{
     void Interact();
}
interface Ignitable
{
     void Ignite();
}

interface Cookable
{
    public void Cook();
}
*/
// interfaces are not really useful in this case

[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public string itemDes;
    public Sprite itemIcon;
    public Sprite itemSprite;
    public GameObject itemObject;
    public int itemAmount;

    public int cookToItem_ID;
    public int igniteToItem_ID;

    public bool isInteractable { get; protected set; }
    public bool isCookable { get; protected set; }
    public bool isIgniteable { get; protected set; }

    public float workTimeNeeded;

    public Item()
    {
        itemName = null;
    }

    public Item(string name)
    {
        itemName = name;

        LoadIcon();
        LoadItemObject();

        //LoadWeaponSprite();
    }

    public void LoadIcon()
    {
        if (ItemDatabase.instance.iconSpriteSheet.ContainsKey(itemName))
            itemIcon = ItemDatabase.instance.iconSpriteSheet[itemName];
        else
            Debug.Log(itemName + "icon not exist in spritesheet");
    }

    public void LoadItemObject()
    {
        if (ItemDatabase.instance.itemObjectsDic.ContainsKey(itemName))
            itemObject = ItemDatabase.instance.itemObjectsDic[itemName];
        else
            Debug.Log(itemName + "item object not exist in assets");
    }

    /*
    public void LoadWeaponSprite()
    {
        if (ItemDatabase.instance.weaponSpriteSheet.ContainsKey(itemName))
            itemSprite = ItemDatabase.instance.weaponSpriteSheet[itemName];
        else
            Debug.Log(itemName + " weaponsprite not exist in spritesheet");

    }
    */

    public virtual int Ignite()
    {
        return igniteToItem_ID;
    }

}
