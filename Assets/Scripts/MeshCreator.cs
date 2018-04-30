using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MeshCreator
{
    private List<Vector3> verts = new List<Vector3>();
    private List<Vector3> normals = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<int> triangleIndices = new List<int>();
    private List<Color> colors = new List<Color>();
    private Mesh mesh;

    public MeshCreator()
    {

    }

    public Mesh CreateMesh()
    {
        mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangleIndices.ToArray();
        mesh.colors = colors.ToArray();
        return mesh;
    }

    public void BuildTriangle(ColoredPoint v0, ColoredPoint v1, ColoredPoint v2)
    {
        Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;
        BuildTriangle(v0, v1, v2, normal);
    }

    public void BuildTriangle(ColoredPoint v0, ColoredPoint v1, ColoredPoint v2, Vector3 normal)
    {
        int v0Index = verts.Count;
        int v1Index = verts.Count + 1;
        int v2Index = verts.Count + 2;

        // add vertices
        verts.Add(v0.pos);
        verts.Add(v1.pos);
        verts.Add(v2.pos);

        // add a surface normal for each vertex
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);

        // add uvs
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 1));

        // add colors
        colors.Add(v0.color);
        colors.Add(v1.color);
        colors.Add(v2.color);

        // add indices that point to vertices
        triangleIndices.Add(v0Index);
        triangleIndices.Add(v1Index);
        triangleIndices.Add(v2Index);
    }

    public void Clear()
    {
        if (mesh != null)
        {
            mesh.Clear();
        }

        verts.Clear();
        normals.Clear();
        uvs.Clear();
        triangleIndices.Clear();
        colors.Clear();
    }
}