using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickManager : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
        // left click
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Collider col = GetRaycastHitObject();

            if (col != null)
                ProcessMouseClick(col);
            //   LocatePosition();
        }

        // right click
        else if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
        }
    }

    private Collider GetRaycastHitObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
            return hit.collider;

        return null;
    }

    private void ProcessMouseClick(Collider obj)
    {
        string tag = obj.tag;

        if (tag == "Terrain")
            ClickOnTerrain(obj);
        else if (tag == "HeatSource")
            ClickOnHeatSource(obj);
    }

    // make these more abstarct
    private void ClickOnTerrain(Collider terrain)
    {
        Inventory inventory = InventoryPanel.instance.inventory;

        // drop item if dragging any
        if (inventory.isDraggingItem)
            inventory.DropItem(inventory.draggedItem);
    }

    private void ClickOnHeatSource(Collider heatSource)
    {
        Inventory inventory = InventoryPanel.instance.inventory;

        if (inventory.isDraggingItem)
        {
            Debug.Log("click on heat source while dragging");
            return;
        }

        Debug.Log("click on heat source");
    }
}
