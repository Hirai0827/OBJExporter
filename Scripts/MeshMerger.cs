using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class MeshMerger
{
    public static Mesh Merge(List<Mesh> meshes,List<Transform> transforms)
    {
        var result = new Mesh();
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        for (int i = 0; i < meshes.Count; i++)
        {
            var mesh = meshes[i];
            var transform = transforms[i];
            var transformedVertices = mesh.vertices.Select(x => transform.localToWorldMatrix * new Vector4(x.x,x.y,x.z,1.0f))
                .Select(x => new Vector3(x.x / x.w, x.y / x.w, x.z / x.w));
            Debug.Log(vertices.Count);
            var offsetTriangles = mesh.triangles.Select(x => x + vertices.Count);
            triangles.AddRange(offsetTriangles);
            vertices.AddRange(transformedVertices);
        }
        var str = "";
        for (int i = 0; i < triangles.Count; i++)
        {
            str += triangles[i].ToString() + ",";
        }
        Debug.Log(str);

        result.SetVertices(vertices);
        result.SetTriangles(triangles,0);
        return result;
    }
}
