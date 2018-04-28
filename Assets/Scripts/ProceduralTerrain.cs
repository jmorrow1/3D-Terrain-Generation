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
        Vector3[,] vertices = new Vector3[width+1, depth+1];
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

                vertices[i,j] = new Vector3(x, y, z);
            }
        }

        // build triangles out of those vertices
        for (int i=0; i<width; i++)
        {
            for (int j=0; j<width; j++)
            {
                Vector3 a = vertices[i, j];
                Vector3 b = vertices[i + 1, j];
                Vector3 c = vertices[i, j + 1];
                Vector3 d = vertices[i + 1, j + 1];

                mc.BuildTriangle(d, b, a);
                mc.BuildTriangle(c, d, a);
            }
        }

        meshFilter.mesh = mc.CreateMesh();
	}
}
