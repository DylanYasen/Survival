using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour
{
    public GameObject building;
    private Transform buildingTrans;
    private Vector3 cachedPos;

    public bool buildingOnHold { get; private set; }

    private RaycastHit hit;
    private Ray ray;
    private Camera mainCam;

    public static BuildingManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        buildingOnHold = false;
        mainCam = Camera.main;

    }

    void Update()
    {
        if (buildingOnHold)
        {
            LocatePosition();
            buildingTrans.position = cachedPos;


            // place input ** need to redesign for mobile
            if (Input.GetMouseButtonDown(0))
            {
                if (isLegalPosition())
                {
                    OnPlaceBuilding();
                }
            }
        }
    }

    private bool isLegalPosition()
    {
        return true;
    }

    private void LocatePosition()
    {
        ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag == "Terrain")
                cachedPos.Set(hit.point.x, 0, hit.point.z);
        }

    }

    public void HoldModel(GameObject building)
    {
        buildingOnHold = true;
        this.building = building;
        buildingTrans = building.transform;
    }

    private void OnPlaceBuilding()
    {
        // 1. place the building
        // 2. unhold the building
        // 3. remove item from inventory
        // 4.
    }

    public void PlaceModel()
    {

    }


}
