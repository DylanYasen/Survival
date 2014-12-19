using UnityEngine;
using System.Collections;

public class PlayerEquipmentController : MonoBehaviour
{
    // gear attach point
    public Transform RightHand;
    public Transform LeftHand;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Equip(EquipableItem.EquipType type, Transform gear)
    {
        switch (type)
        {
            case EquipableItem.EquipType.Weapon_RightHand:
                gear.parent = RightHand;
                break;

            case EquipableItem.EquipType.Weapon_LeftHand:
                gear.parent = LeftHand;
                break;

        }

        gear.localPosition = Vector3.zero;
        gear.localEulerAngles = Vector3.zero;
        gear.localScale = gear.localScale;
    }

    public void UnEquip(EquipableItem.EquipType type)
    {
        Transform gear = transform; // just for init

        switch (type)
        {
            case EquipableItem.EquipType.Weapon_RightHand:
                gear = RightHand.GetChild(0);
                gear.parent = ItemDatabase.instance.transform;
                break;

            case EquipableItem.EquipType.Weapon_LeftHand:
                gear = LeftHand.GetChild(0);
                gear.parent = ItemDatabase.instance.transform;

                break;
        }


        ItemPoolManager.instance.ReturnPool(gear.gameObject);
    }

}
