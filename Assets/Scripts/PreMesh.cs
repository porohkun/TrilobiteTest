using System.Collections.Generic;
using UnityEngine;

public class PreMesh
{
    public Vector3[] Vertices;
    public Vector3[] Normals;
    public Vector2[] Uv;
    public int[] Triangles;

    public static Mesh ToMesh(IEnumerable<PreMesh> list)
    {
        List<Vector3> v = new List<Vector3>();
        List<Vector3> n = new List<Vector3>();
        List<Vector2> u = new List<Vector2>();
        List<int> t = new List<int>();
        int tr = 0;

        foreach (var item in list)
        {
            v.AddRange(item.Vertices);
            n.AddRange(item.Normals);
            u.AddRange(item.Uv);
            foreach (int triangle in item.Triangles)
                t.Add(tr + triangle);
            tr = v.Count;
        }
        return new Mesh
        {
            vertices = v.ToArray(),
            normals = n.ToArray(),
            uv = u.ToArray(),
            triangles = t.ToArray()
        };
    }
}

