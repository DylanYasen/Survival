﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance { get; private set; }
    public Dictionary<string, Sprite> iconSpriteSheet { get; private set; }
    public Dictionary<string, Sprite> weaponSpriteSheet { get; private set; }

    private Sprite[] icons;
    private Sprite[] weaponSprites;

    public List<Item> items { get; private set; }

    void Awake()
    {
        instance = this;

        items = new List<Item>();

        // init icon spritesheet;
        icons = Resources.LoadAll<Sprite>("itemicon");
        iconSpriteSheet = new Dictionary<string, Sprite>();

        // store item icon in dictionary
        for (int i = 0; i < icons.Length; i++)
            iconSpriteSheet.Add(icons[i].name, icons[i]);


        // init weapon spritesheet
        weaponSprites = Resources.LoadAll<Sprite>("itemSprite");
        weaponSpriteSheet = new Dictionary<string, Sprite>();

        // store item sprite in dictionary
        for (int i = 0; i < weaponSprites.Length; i++)
            weaponSpriteSheet.Add(weaponSprites[i].name, weaponSprites[i]);

        // items
        int itemIDCounter = -1;

        EquipableItem axe = new EquipableItem();
        axe.itemName = "Axe";
        axe.itemID = ++itemIDCounter;
        axe.itemDes = "mighty axe";
        axe.itemAmount = 1;
        axe.equipType = EquipableItem.EquipType.Weapon;
        axe.itemEffects.Add(new OnEquipAddDmg(2));
        axe.InitEffect();
        axe.LoadIcon();
        axe.LoadWeaponSprite();
        items.Add(axe);

        EquipableItem alchemistPotion = new EquipableItem();
        alchemistPotion.itemName = "Alchemist Potion";
        alchemistPotion.itemID = ++itemIDCounter;
        alchemistPotion.itemDes = "Alchemist Potion";
        alchemistPotion.itemAmount = 1;
        alchemistPotion.equipType = EquipableItem.EquipType.Weapon;
        alchemistPotion.itemEffects.Add(new OnEquipAddDmg(2));
        alchemistPotion.InitEffect();
        alchemistPotion.LoadIcon();
        alchemistPotion.LoadWeaponSprite();
        items.Add(alchemistPotion);

        EquipableItem Khopesh = new EquipableItem();
        Khopesh.itemName = "Khopesh";
        Khopesh.itemID = ++itemIDCounter;
        Khopesh.itemDes = "Khopesh";
        Khopesh.itemAmount = 1;
        Khopesh.equipType = EquipableItem.EquipType.Weapon;
        Khopesh.itemEffects.Add(new OnEquipAddDmg(2));
        Khopesh.InitEffect();
        Khopesh.LoadIcon();
        Khopesh.LoadWeaponSprite();
        items.Add(Khopesh);

        EquipableItem Rapier = new EquipableItem();
        Rapier.itemName = "Rapier";
        Rapier.itemID = ++itemIDCounter;
        Rapier.itemDes = "Rapier";
        Rapier.itemAmount = 1;
        Rapier.equipType = EquipableItem.EquipType.Weapon;
        Rapier.itemEffects.Add(new OnEquipAddDmg(2));
        Rapier.InitEffect();
        Rapier.LoadIcon();
        Rapier.LoadWeaponSprite();
        items.Add(Rapier);

        EquipableItem Sword = new EquipableItem();
        Sword.itemName = "Sword";
        Sword.itemID = ++itemIDCounter;
        Sword.itemDes = "Sword";
        Sword.itemAmount = 1;
        Sword.equipType = EquipableItem.EquipType.Weapon;
        Sword.itemEffects.Add(new OnEquipAddDmg(2));
        Sword.InitEffect();
        Sword.LoadIcon();
        Sword.LoadWeaponSprite();
        items.Add(Sword);

        EquipableItem WizardStaff = new EquipableItem();
        WizardStaff.itemName = "Wizard Staff";
        WizardStaff.itemID = ++itemIDCounter;
        WizardStaff.itemDes = "Wizard Staff";
        WizardStaff.itemAmount = 1;
        WizardStaff.equipType = EquipableItem.EquipType.Weapon;
        WizardStaff.itemEffects.Add(new OnEquipAddDmg(2));
        WizardStaff.InitEffect();
        WizardStaff.LoadIcon();
        WizardStaff.LoadWeaponSprite();
        items.Add(WizardStaff);

        EquipableItem Gun = new EquipableItem();
        Gun.itemName = "Gun";
        Gun.itemID = ++itemIDCounter;
        Gun.itemDes = "Gun";
        Gun.itemAmount = 1;
        Gun.equipType = EquipableItem.EquipType.Weapon;
        Gun.itemEffects.Add(new OnEquipAddDmg(2));
        Gun.InitEffect();
        Gun.LoadIcon();
        Gun.LoadWeaponSprite();
        items.Add(Gun);

        ConsumableItem rawTurkey = new ConsumableItem();
        rawTurkey.itemName = "Raw Turkey";
        rawTurkey.itemID = ++itemIDCounter;
        rawTurkey.itemDes = "Surprisingly nourishing, this item heals the player character for 2 points.";
        rawTurkey.itemAmount = 1;
        rawTurkey.itemEffects.Add(new OnUseHeal(2));
        rawTurkey.InitEffect();
        rawTurkey.LoadIcon();
        items.Add(rawTurkey);

        ConsumableItem refreshingWater = new ConsumableItem();
        refreshingWater.itemName = "Refreshing Water";
        refreshingWater.itemID = ++itemIDCounter;
        refreshingWater.itemDes = "Soothingly hydrating, this item immediately gives the player 4 SP.";
        refreshingWater.itemAmount = 1;
        refreshingWater.itemEffects.Add(new OnUseRecoverSP(4));
        refreshingWater.InitEffect();
        refreshingWater.LoadIcon();
        items.Add(refreshingWater);

    }
}