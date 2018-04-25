using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MeshCreator
{
    private List<Vector3> verts = new List<Vector3>();
    private List<Vector3> normals = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private List<Color> colors = new List<Color>();
    private List<int> triangleIndices = new List<int>();
    private Mesh mesh;
    public int vertexCount { get { return verts.Count; } }

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

    public void BuildTriangle(int i0, int i1, int i2)
    {
        triangleIndices.Add(i0);
        triangleIndices.Add(i1);
        triangleIndices.Add(i2);
    }

    public void AddVertex(Vector3 v, Vector3 normal, Vector2 uv, Color color)
    {
        verts.Add(v);
        normals.Add(normal);
        uvs.Add(uv);
        colors.Add(color);
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