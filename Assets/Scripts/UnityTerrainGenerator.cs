using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTerrainGenerator : MonoBehaviour 
{
    public int width = 256;
    public int height = 20;
    public int depth = 256;
    public float noiseScale = 0.1f;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = 256;
        terrainData.size = new Vector3(width, height, depth);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, depth];
        for (int i=0; i<width; i++)
        {
            float x = i * noiseScale;
            for (int j=0; j<height; j++)
            {
                float z = j * noiseScale;
                float y = Perlin.Noise(i * noiseScale, j * noiseScale);
                heights[i, j] = y;
            }
        }
        Debug.Log(height);
        return heights;
    }
}
