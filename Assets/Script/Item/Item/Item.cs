using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Item
{
    public string itemName;
    public int itemID;
    public string itemDes;
    public Sprite itemIcon;
    public Sprite itemSprite;
    public int itemAmount;

    public bool isInteractive;
    public bool isIgnitable;

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


}
