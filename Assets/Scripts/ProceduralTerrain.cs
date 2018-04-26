using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class ProceduralTerrain : MonoBehaviour 
{
    public Vector3 noiseScale; 
    [Range(0, 255)] public int width;
    [Range(0, 255)] public int depth;
    public float maxHeight;
    public float squareSize;
    private MeshCreator mc;
    private MeshFilter meshFilter;

	void Start () 
	{
        mc = new MeshCreator();
        meshFilter = GetComponent<MeshFilter>();
	}
	
	void Update () 
	{
        mc.Clear();

        // add vertices
        for (int i=0; i<=width; i++)
        {
            float x = i * squareSize;
            for (int j=0; j<=depth; j++)
            {
                float z = j * squareSize;

                // compute y-value using noise
                float noiseOut = Perlin.Noise(noiseScale.x * i, noiseScale.y * Time.time, noiseScale.z * j);
                noiseOut += 1;
                noiseOut /= 2;
                float y = noiseOut * maxHeight;

                // compute uv
                float u = (float)i / width;
                float v = (float)j / depth;

                // add the vertex
                Color color = Random.value < 0.5 ? Color.blue : Color.green;
                mc.AddVertex(new Vector3(x, y, z), Vector3.up, new Vector2(u, v), color);
            }
        }

        // build triangles out of those vertices
        {
            int i = 0;
            while (i + depth + 1 < mc.vertexCount)
            {
                mc.BuildTriangle(i, i + depth, i + depth + 1);
                mc.BuildTriangle(i, i + depth + 1, i + 1);
                i++;
            }
        }

        meshFilter.mesh = mc.CreateMesh();
	}
}
