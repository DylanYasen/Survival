using UnityEngine;
using System.Collections;

public class BuildableItem : Item
{
    public float needTime;
    public GameObject buildingModel;

    private bool isBuilding;

    public BuildableItem(string name)
        : base(name)
    {
        buildingModel = Resources.Load(Constants.buildingPrefabPathPrefix + name) as GameObject;
    }

    public void Build()
    {
        isBuilding = true;
    }

    void Update()
    {
        if (!isBuilding)
            return;
    }


    public void OnBuild(Vector3 position)
    {
        if (PhotonNetwork.offlineMode)
        {
            // instantiate directly
        }
        else
        {
            PhotonNetwork.Instantiate(Constants.buildingPrefabPathPrefix + itemName, position, Quaternion.identity, 0);
        }
    }
}
