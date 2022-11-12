using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlateControllerBase : MonoBehaviour
{
    protected MeshFilter meshFilter;
    public MeshFilter MeshFilter { get => meshFilter ?? GetComponent<MeshFilter>(); }
}
