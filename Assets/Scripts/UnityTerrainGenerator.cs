using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityTerrainGenerator : MonoBehaviour 
{
    private int width = 256;
    private int depth = 256;
    private Terrain terrain;

    [Range(0, 100)]
    public int height = 20;

    [Range(0, 0.5f)]
    public float xzScale = 0.1f;

    [Range(0, 5)]
    public float timeScale = 0.1f;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    void Update()
    {
        terrain.terrainData.size = new Vector3(width, height, depth);
        terrain.terrainData.SetHeights(0, 0, GenerateHeights());
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
        float noiseY = timeScale * Time.time;
        for (int i=0; i<width; i++)
        {
            float noiseX = i * xzScale;
            for (int j=0; j<depth; j++)
            {
                float noiseZ = j * xzScale;
                float y = Perlin.Noise(noiseX, noiseY, noiseZ);
                heights[i, j] = y;
            }
        }
        return heights;
    }
}
