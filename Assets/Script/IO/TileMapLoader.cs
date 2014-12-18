using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

[ExecuteInEditMode]
public class TileMapLoader : MonoBehaviour
{
    public string LevelName;
    private string MapTheme;

    private string MapDataRaw;
    private JSONNode MapData;

    private int tileAmount;

    public bool LoadMap()
    {
        if (File.Exists("Assets/Resources/Maps/" + LevelName + ".inheritanceMap"))
        {
            var file = File.OpenText("Assets/Resources/Maps/" + LevelName + ".inheritanceMap");
            MapDataRaw = file.ReadToEnd();
            Debug.Log(MapDataRaw);
            file.Close();

            MapData = DecodeMapData();
            return true;
        }
        else
        {
            Debug.Log(LevelName + " doesn't exist.");
            return false;
        }
    }


    public void CreateMap()
    {
        MapTheme = MapData["MapTheme"];

        var parentObj = new GameObject();
        parentObj.name = LevelName;
        Transform parentTrans = parentObj.transform;

        GameObject prefab;
        GameObject obj;
        Transform t;
        SpriteRenderer sr;
        Vector3 tilePos = new Vector3();

        tileAmount = MapData["Tiles"].Count;
        for (int i = 0; i < tileAmount; i++)
        {
            string tileName = MapData["Tiles"][i]["TileName"];
            float x = float.Parse(MapData["Tiles"][i]["TileXPosition"]);
            float y = float.Parse(MapData["Tiles"][i]["TileYPosition"]);
            float rotationZ = float.Parse(MapData["Tiles"][i]["TileRotation"]);
            string sortingLayer = MapData["Tiles"][i]["TileSortingLayer"];
            int sortingOrder = int.Parse(MapData["Tiles"][i]["TileSortingOrder"]);

            prefab = Resources.Load("TilePrefabs/" + MapTheme + "/" + tileName) as GameObject;

            obj = Instantiate(prefab) as GameObject;
            obj.name = tileName;

            t = obj.transform;
            sr = obj.GetComponent<SpriteRenderer>();

            //t.position = new Vector3(Mathf.Round(x * 100), Mathf.Round(y * 100), 0);
            tilePos.Set(x, y, 0);
            t.position = tilePos;
            t.parent = parentTrans;

            t.localEulerAngles = new Vector3(0, 0, rotationZ);
            sr.sortingLayerName = sortingLayer;
            sr.sortingOrder = sortingOrder;
        }
    }

    JSONNode DecodeMapData()
    {
        var data = JSONNode.LoadFromBase64(MapDataRaw);

        Debug.Log(data.ToString());

        return data;
    }
}
