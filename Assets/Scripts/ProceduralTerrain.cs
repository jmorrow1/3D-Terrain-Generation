using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class ProceduralTerrain : MonoBehaviour 
{
    public Color maxColor = Color.blue;
    public Color minColor = Color.green;

    public const int MAX_WIDTH = 100;
    public const int MAX_HEIGHT = 100;
    public Vector3 noiseScale; 
    [Range(0, MAX_WIDTH)] public int width;
    [Range(0, MAX_HEIGHT)] public int depth;
    public float maxHeight;
    public float squareSize;
    private MeshCreator mc;
    private MeshFilter meshFilter;
    private ColoredPoint[,] vertices;

	void Start () 
	{
        mc = new MeshCreator();
        meshFilter = GetComponent<MeshFilter>();
        vertices = new ColoredPoint[MAX_WIDTH+1, MAX_HEIGHT+1];
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

                // compute noise at given space time point
                float noiseOut = Perlin.Noise(noiseScale.x * i, noiseScale.y * Time.time, noiseScale.z * j);

                // normalize noise
                noiseOut += 1;
                noiseOut /= 2;

                // compute y-value from noise
                float y = noiseOut * maxHeight;

                // compute uv
                float u = (float)i / width;
                float v = (float)j / depth;

                // compute color from noise

                Color color = minColor + noiseOut * (maxColor - minColor);

                // add the vertex
                vertices[i,j] = new ColoredPoint(x, y, z, color.r, color.g, color.b);
            }
        }

        // build triangles out of those vertices
        for (int i=0; i<width; i++)
        {
            for (int j=0; j<depth; j++)
            {
                ColoredPoint a = vertices[i, j];
                ColoredPoint b = vertices[i + 1, j];
                ColoredPoint c = vertices[i, j + 1];
                ColoredPoint d = vertices[i + 1, j + 1];

                mc.BuildTriangle(d, b, a);
                mc.BuildTriangle(c, d, a);
            }
        }

        meshFilter.mesh = mc.CreateMesh();
	}
}
