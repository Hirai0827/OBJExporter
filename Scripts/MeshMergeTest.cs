using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshMergeTest : MonoBehaviour
{
    [SerializeField]
    private List<MeshFilter> meshFilters;
    [SerializeField]
    private MeshFilter mergedMeshFilter;

    [SerializeField] private MeshFilter reconstructMeshFilter;

    private void Awake()
    {
        mergedMeshFilter.mesh = MeshMerger.Merge(meshFilters.Select(x => x.mesh).ToList(),meshFilters.Select(x => x.transform).ToList());
        reconstructMeshFilter.mesh = MeshImporter.ImportObj(MeshExporter.ExportAsObj(mergedMeshFilter.mesh));
    }
}
