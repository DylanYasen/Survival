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


public class Item
{
    public string itemName;
    public int itemID;
    public string itemDes;
    public Sprite itemIcon;
    public Sprite itemSprite;
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
        LoadWeaponSprite();
    }

    public void LoadIcon()
    {
        if (ItemDatabase.instance.iconSpriteSheet.ContainsKey(itemName))
            itemIcon = ItemDatabase.instance.iconSpriteSheet[itemName];
        else
            Debug.Log(itemName + "icon not exist in spritesheet");
    }

    public void LoadWeaponSprite()
    {
        if (ItemDatabase.instance.weaponSpriteSheet.ContainsKey(itemName))
            itemSprite = ItemDatabase.instance.weaponSpriteSheet[itemName];
        else
            Debug.Log(itemName + " weaponsprite not exist in spritesheet");

    }


    public virtual int Ignite()
    {
        return igniteToItem_ID;
    }

}
