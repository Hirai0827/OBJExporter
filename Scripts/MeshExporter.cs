using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshExporter
{
    public static string ExportAsObj(Mesh mesh)
    {
        return ToText(mesh, 0);
    }

    private static string ToText(Mesh mesh, int subMeshIndex)
    {
        return ToText(mesh.vertices, mesh.uv, mesh.normals, mesh.GetIndices(subMeshIndex));
    }

    private static string ToText(
        IList<Vector3> positions,
        IList<Vector2> uvs,
        IList<Vector3> normals,
        IList<int> indices
        )
    {
        var sb = new System.Text.StringBuilder();
        Debug.Assert(positions != null);
        foreach (var item in positions)
        {
            sb.AppendFormat("v {0} {1} {2}\n",
                item.x.ToString("F8"), //精度指定しないとfloat精度の全体を吐かないので劣化してしまう。10進8桁必要
                item.y.ToString("F8"),
                item.z.ToString("F8"));
        }

        bool hasUv = (uvs != null) && (uvs.Count > 0);
        if (hasUv)
        {
            Debug.Assert(uvs.Count == positions.Count);
            foreach (var item in uvs)
            {
                sb.AppendFormat("vt {0} {1}\n",
                    item.x.ToString("F8"),
                    item.y.ToString("F8"));
            }
        }

        Debug.Assert(normals != null);
        foreach (var item in normals)
        {
            sb.AppendFormat("vn {0} {1} {2}\n",
                item.x.ToString("F8"),
                item.y.ToString("F8"),
                item.z.ToString("F8"));
        }

        Debug.Assert(indices != null);
        Debug.Assert((indices.Count % 3) == 0);
        for (var i = 0; i < indices.Count; i += 3)
        {
            var i0 = indices[i + 0] + 1; // 1 based index.
            var i1 = indices[i + 1] + 1;
            var i2 = indices[i + 2] + 1;
            if (hasUv)
            {
                sb.AppendFormat("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                    i0,
                    i1,
                    i2);
            }
            else
            {
                sb.AppendFormat("f {0}//{0} {1}//{1} {2}//{2}\n",
                    i0,
                    i1,
                    i2);
            }
        }

        return sb.ToString();
    }
}