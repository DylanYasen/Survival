using UnityEngine;
using System.Collections;

public class PlayerEquipmentController : MonoBehaviour
{
    // gear attach point
    public Transform RightHand;
    public Transform LeftHand;

    public PhotonView m_pv;

    void Start()
    {
        m_pv = GetComponent<Player>().m_photonView;
    }

    void Update()
    {

    }

    [RPC]
    public void Equip(int type, string itemName)
    {

        GameObject gear = PhotonNetwork.Instantiate("Prefab/Items/" + itemName, Vector3.zero, Quaternion.identity, 0);
        gear.GetComponent<Pickup>().enabled = false;

        switch (type)
        {
            case (int)EquipableItem.EquipType.Weapon_RightHand:
                gear.transform.parent = RightHand;
                break;

            case (int)EquipableItem.EquipType.Weapon_LeftHand:
                gear.transform.parent = LeftHand;
                break;
        }

        ResetTrans(gear.transform);
    }

    [RPC]
    public void UnEquip(int type)
    {
        Transform gear = transform; // just for init

        switch (type)
        {
            case (int)EquipableItem.EquipType.Weapon_RightHand:
                gear = RightHand.GetChild(0);
                gear.parent = ItemDatabase.instance.transform;
                break;

            case (int)EquipableItem.EquipType.Weapon_LeftHand:
                gear = LeftHand.GetChild(0);
                gear.parent = ItemDatabase.instance.transform;

                break;
        }

        ResetTrans(gear);

        //ItemPoolManager.instance.ReturnPool(gear.gameObject);
    }


    private void ResetTrans(Transform t)
    {
        t.localEulerAngles = Vector3.zero;
        t.localPosition = Vector3.zero;
        t.localScale = t.localScale;
    }

}
