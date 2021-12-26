using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshImporter
{
    public static Mesh ImportObj(string objstr)
    {
        var sentences = objstr.Split('\n');
        var vertices = new List<Vector3>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();
        var triangles = new List<int>();
        for (int i = 0; i < sentences.Length; i++)
        {
            var sentence = sentences[i];
            var tokens = Parse(sentence);
            switch (tokens[0])
            {
                case "v":
                    vertices.Add(ComposeVectorFromString(tokens[1],tokens[2],tokens[3]));
                    break;
                case "vt":
                    uvs.Add(ComposeVectorFromString(tokens[1],tokens[2]));
                    break;
                case "vn":
                    normals.Add(ComposeVectorFromString(tokens[1],tokens[2],tokens[3]));
                    break;
                case "f":
                    triangles.Add(ParseFaceIndex(tokens[1]));
                    triangles.Add(ParseFaceIndex(tokens[2]));
                    triangles.Add(ParseFaceIndex(tokens[3]));
                    break;
            }
        }

        var result = new Mesh();
        result.SetVertices(vertices);
        result.SetNormals(normals);
        result.SetUVs(0,uvs);
        result.SetTriangles(triangles,0);
        return result;
    }

    private static string[] Parse(string sentence)
    {
        return sentence.Split(' ');
    }

    private static int ParseFaceIndex(string faceStr)
    {
        var indexStr = faceStr.Split('/')[0];
        return int.Parse(indexStr) - 1;
    }

    private static Vector3 ComposeVectorFromString(string xStr,string yStr,string zStr)
    {
        var x = float.Parse(xStr);
        var y = float.Parse(yStr);
        var z = float.Parse(zStr);
        return new Vector3(x, y, z);
    }

    private static Vector2 ComposeVectorFromString(string xStr,string yStr)
    {
        var x = float.Parse(xStr);
        var y = float.Parse(yStr);
        return new Vector2(x, y);
    }
    
}
