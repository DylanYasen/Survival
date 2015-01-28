using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour
{
    private GameObject building;
    private Transform buildingTrans;
    private Vector3 cachedPos;

    public bool isBuildingOnHold { get; private set; }

    private RaycastHit hit;
    private Ray ray;
    private Camera mainCam;

    public static BuildingManager instance;

    private int onHoldItemSlotNum;
    private BuildableItem onHoldItem;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isBuildingOnHold)
        {
            LocatePosition();
            buildingTrans.position = cachedPos;

            // place input ** need to redesign for mobile
            if (Input.GetMouseButtonDown(0))
                if (isLegalPosition())
                    PlaceBuilding();
        }
    }

    public void HoldBuilding(BuildableItem item, int slotNum)
    {
        mainCam = Camera.main;
        onHoldItem = item;
        onHoldItemSlotNum = slotNum;

        // create building
        this.building = Instantiate(item.buildingModel, Vector2.zero, Quaternion.identity) as GameObject;
        buildingTrans = building.transform;

        // update toggle
        isBuildingOnHold = true;
    }


    private bool isLegalPosition()
    {
        return true;
    }

    private void LocatePosition()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
            //if (hit.collider.tag == "Terrain")
            cachedPos.Set(hit.point.x, 0, hit.point.z);
    }

    private void PlaceBuilding()
    {
        // 1. place the building
        // 2. unhold the building
        // 3. remove item from inventory
        // 4.

        onHoldItem.OnBuild(buildingTrans.position);

        Destroy(building);

        isBuildingOnHold = false;

        // remove from inventory
        InventoryPanel.instance.inventory.RemoveItemAtSlot(onHoldItemSlotNum);
        onHoldItemSlotNum = -1;
        onHoldItem = null;

    }

}
