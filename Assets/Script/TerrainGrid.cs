using UnityEngine;
using System.Collections;

public class TerrainGrid : MonoBehaviour
{
    public Terrain terrain;
    public TerrainData terrainData;

    public int terrainWidth;
    public int terrainHeight;

    public Vector2[,] terrainGrid { get; private set; }

    private Vector2 vec2;

    void Start()
    {
        terrain = Terrain.activeTerrain;
        terrainData = terrain.terrainData;

        terrainWidth = terrainData.alphamapWidth;
        terrainHeight = terrainData.heightmapHeight;

        Debug.Log(terrainWidth);
        Debug.Log(terrainHeight);

        terrainGrid = new Vector2[terrainWidth, terrainHeight];

        SetUpGridArray();

    }

    void SetUpGridArray()
    {
        for (int i = 0; i < terrainWidth; i++)
        {
            for (int j = 0; j < terrainHeight; j++)
            {
                vec2.Set(i, j);
                terrainGrid[i, j] = vec2;
            }
        }
    }
}
