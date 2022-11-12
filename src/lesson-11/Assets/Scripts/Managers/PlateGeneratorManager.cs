using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlateGeneratorManager : MonoBehaviour
{
    private static PlateGeneratorManager manager;
    public static PlateGeneratorManager Manager { get => manager; }

    private struct ChunkData
    {
        public Vector3 size;
        public Vector3 center;
    }

    private struct ChunkOrder
    {
        public ChunkAxis axis;
        public ChunkOffset offset;

        public ChunkOrder(ChunkAxis ax, ChunkOffset off)
        {
            axis = ax;
            offset = off;
        }
    }

    private ChunkOrder[] chunkOrder;

    private enum ChunkAxis { X, Z }

    private enum ChunkOffset { neg = -1, pos = 1 }    

    [SerializeField] private SpawnedPlateController movingPlatePrefab;
    [SerializeField] private StaticPlateController staticPlatePrefab;
    [SerializeField] private RuinedPlateController ruinedPlatePrefab;

    private PlateControllerBase plateToSpawn;

    [SerializeField] private Vector3 meshDimensions = new Vector3(10.0f, 1.0f, 10.0f);
    public Vector3 MeshDimensions { get => meshDimensions; }

    [SerializeField] private Vector3 minMeshDimensions = new Vector3(0.2f, 0.2f, 0.2f);
    public Vector3 MinMeshDimensions { get => minMeshDimensions; }

    private StaticPlateController previousPlate;

    private PlateControllerBase activePlate;

    private Vector3 prevSize;
    private Vector3 activeSize;
    private Vector3 prevCenter;
    private Vector3 activeCenter;

    private bool isGeneratingPossible = true;
    public bool IsGeneratingPossible { get => isGeneratingPossible; }

    private void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }

        chunkOrder = new ChunkOrder[4]
        {
            new ChunkOrder(ChunkAxis.X, ChunkOffset.neg),
            new ChunkOrder(ChunkAxis.X, ChunkOffset.pos),
            new ChunkOrder(ChunkAxis.Z, ChunkOffset.neg),
            new ChunkOrder(ChunkAxis.Z, ChunkOffset.pos)
        };
    }

    private void Update()
    {
        
    }

    private Vector3[] GetVertices()
    {
        
        float xValue = meshDimensions.x / 2.0f;
        float yValue = meshDimensions.y / 2.0f;
        float zValue = meshDimensions.z / 2.0f;

        Vector3[] verts = {
            // front: 0, 1, 2, 3
            new Vector3(-xValue, -yValue, zValue),
            new Vector3(-xValue, yValue, zValue),
            new Vector3(xValue, yValue, zValue),
            new Vector3(xValue, -yValue, zValue),

            // left: 0(4), 4(5), 5(6), 1(7)
            new Vector3(-xValue, -yValue, zValue),
            new Vector3(-xValue, -yValue, -zValue),
            new Vector3(-xValue, yValue, -zValue),
            new Vector3(-xValue, yValue, zValue),

            // back: 4(8), 7(9), 6(10), 5(11)
            new Vector3(-xValue, -yValue, -zValue),
            new Vector3(xValue, -yValue, -zValue),
            new Vector3(xValue, yValue, -zValue),
            new Vector3(-xValue, yValue, -zValue),

            // right: 3(12), 2(13), 6(14), 7(15)
            new Vector3(xValue, -yValue, zValue),
            new Vector3(xValue, yValue, zValue),
            new Vector3(xValue, yValue, -zValue),
            new Vector3(xValue, -yValue, -zValue),

            // top: 1(16), 5(17), 6(18), 2(19)
            new Vector3(-xValue, yValue, zValue),
            new Vector3(-xValue, yValue, -zValue),
            new Vector3(xValue, yValue, -zValue),
            new Vector3(xValue, yValue, zValue),

            // bottom: 0(20), 3(21), 7(22), 4(23)
            new Vector3(-xValue, -yValue, zValue),
            new Vector3(xValue, -yValue, zValue),
            new Vector3(xValue, -yValue, -zValue),
            new Vector3(-xValue, -yValue, -zValue)

        };


        return verts;
        
    }

    private int[] GetTriangles()
    {
       int[] triangles = {
            // front
            0, 3, 2,
            2, 1, 0,

            // left
            4, 7, 6,
            6, 5, 4,

            // back
            8, 11, 10,
            10, 9, 8,

            // right
            12, 15, 14,
            14, 13, 12,

            // top
            16, 19, 18,
            18, 17, 16,

            // bottom
            20, 23, 22,
            22, 21, 20,
        };

        return triangles;        
    }

    private Vector3[] GetNormals()
    {
        Vector3[] norms =
        {
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.left,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.back,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.right,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.up,
            Vector3.down,
            Vector3.down,
            Vector3.down,
            Vector3.down
        };
        return norms;        
    }

    public void GenerateMovingPlate()
    {
        if (isGeneratingPossible)
        {
            activePlate = Instantiate(movingPlatePrefab, GameManager.Manager.ActiveSpawner.transform.position, GameManager.Manager.TowerController.transform.rotation);
            GenerateMesh(activePlate.MeshFilter);
        }       
    }

    private ChunkData GetChunkData(ChunkAxis axis, ChunkOffset chunkOffset)
    {
        int offset = (int) chunkOffset;
        ChunkData chunk = new ChunkData();
        chunk.size = Vector3.zero;
        chunk.center = Vector3.zero;

        float prevRadius = 0.0f, activeRadius = 0.0f, prevCenterCoord = 0.0f, activeCenterCoord = 0.0f;
        float point1, point2;

        Vector3 tmpCenter = Vector3.zero;
        tmpCenter.y = activeCenter.y;

        switch (axis)
        {
            case ChunkAxis.X:
                prevRadius = prevSize.x;
                activeRadius = activeSize.x;
                prevCenterCoord = prevCenter.x;
                activeCenterCoord = activeCenter.x;                
                tmpCenter.z = (activeCenter.z + prevCenter.z) / 2;                
                break;

            case ChunkAxis.Z:
                prevRadius = prevSize.z;
                activeRadius = activeSize.z;
                prevCenterCoord = prevCenter.z;
                activeCenterCoord = activeCenter.z;
                tmpCenter.x = prevCenter.x;
                break;
        }

        prevRadius /= 2;
        activeRadius /= 2;

        point1 = prevCenterCoord + offset * prevRadius;
        point2 = activeCenterCoord + offset * activeRadius;

        switch (axis)
        {
            case ChunkAxis.X:
                chunk.center += tmpCenter;
                chunk.center.x = (point1 + point2) / 2;

                if ((chunkOffset == ChunkOffset.neg && point2 < point1) || (chunkOffset == ChunkOffset.pos && point2 > point1))
                {
                    chunk.size = new Vector3(
                        Mathf.Abs(point2 - point1),
                        activeSize.y,
                        activeSize.z
                        );
                }
                break;

            case ChunkAxis.Z:
                chunk.center += tmpCenter;
                chunk.center.z = (point1 + point2) / 2;
                if ((chunkOffset == ChunkOffset.neg && point2 < point1) || (chunkOffset == ChunkOffset.pos && point2 > point1))
                {
                    chunk.size = new Vector3(
                        prevSize.x,
                        activeSize.y,
                        Mathf.Abs(point2 - point1)
                        );
                }
                break;
        }

        return chunk;
    }

    public StaticPlateController GenerateStaticPlate()
    {
        StaticPlateController staticPlate;

        if (GameManager.Manager.TowerController.TowerPlates.Count > 0)
        {
            SetPlatesVariables();
            Vector3 newStaticSize = activeSize;
            Vector3 newStaticCenter = Vector3.zero;

            foreach (ChunkOrder chunkConfig in chunkOrder)
            {
                ChunkData chunk = GetChunkData(chunkConfig.axis, chunkConfig.offset);
                if (chunk.size != Vector3.zero)
                {
                    if (chunkConfig.axis == ChunkAxis.X)
                    {
                        newStaticSize.x -= chunk.size.x;
                    }

                    if (chunkConfig.axis == ChunkAxis.Z)
                    {
                        newStaticSize.z -= chunk.size.z;
                    }
                    RuinedPlateController ruinedChunk = Instantiate(ruinedPlatePrefab, chunk.center, GameManager.Manager.TowerController.transform.rotation);
                    meshDimensions = chunk.size;
                    GenerateMesh(ruinedChunk.MeshFilter);
                }
            }

            float sizeDeltaX = (activeSize.x - newStaticSize.x) / 2;
            float sizeDeltaZ = (activeSize.z - newStaticSize.z) / 2;
            newStaticCenter.x = prevCenter.x > activeCenter.x ? (prevCenter.x - sizeDeltaX) : (prevCenter.x + sizeDeltaX);
            newStaticCenter.y = activePlate.transform.position.y;
            newStaticCenter.z = prevCenter.z > activeCenter.z ? (prevCenter.z - sizeDeltaZ) : (prevCenter.z + sizeDeltaZ);
            Debug.Log("PrevCenter: " + prevCenter);
            Debug.Log("NewCenter: " + newStaticCenter);

            staticPlate = Instantiate(staticPlatePrefab, newStaticCenter, GameManager.Manager.TowerController.transform.rotation);
            meshDimensions = newStaticSize;
            GenerateMesh(staticPlate.MeshFilter);

        }
        else
        {
            staticPlate = Instantiate(staticPlatePrefab, activePlate.transform.position, activePlate.transform.rotation);
            staticPlate.MeshFilter.mesh = activePlate.MeshFilter.mesh;
        }

        Destroy(activePlate.gameObject);
        isGeneratingPossible = CheckGeneratingPossibility();
        return staticPlate;
    }

    private void GenerateMesh(MeshFilter plateMF)
    {
        Mesh generatedMesh = new Mesh();
        generatedMesh.vertices = GetVertices();
        generatedMesh.triangles = GetTriangles();
        generatedMesh.normals = GetNormals();
        
        plateMF.mesh.Clear();
        plateMF.mesh = generatedMesh;
        plateMF.mesh.Optimize();        
    }

    private bool CheckGeneratingPossibility()
    {
        if (meshDimensions.x <= minMeshDimensions.x || meshDimensions.y <= minMeshDimensions.y || meshDimensions.z <= minMeshDimensions.z)
        {
            return false;
        }
        return true;
    }

    public bool CheckPlatesOverlapping()
    {
        if (GameManager.Manager.TowerController.TowerPlates.Count > 0)
        {
            SetPlatesVariables();
            float activeRadiusX = activeSize.x / 2;
            float activeRadiusZ = activeSize.z / 2;
            float prevRadiusX = prevSize.x / 2;
            float prevRadiusZ = prevSize.z / 2;
            
            Vector4 activeBorders = new Vector4(
                activeCenter.x - activeRadiusX,
                activeCenter.x + activeRadiusX,
                activeCenter.z - activeRadiusZ,
                activeCenter.z + activeRadiusZ
                );
            Vector4 prevBorders = new Vector4(
                prevCenter.x - prevRadiusX,
                prevCenter.x + prevRadiusX,
                prevCenter.z - prevRadiusZ,
                prevCenter.z + prevRadiusZ
                );
            

            bool overlapX = (activeBorders.x < prevBorders.x && activeBorders.y > prevBorders.x) || (activeBorders.y > prevBorders.x && activeBorders.x < prevBorders.y) || (activeBorders.x < prevBorders.x && activeBorders.y > prevBorders.y);
            bool overlapZ = (activeBorders.z < prevBorders.z && activeBorders.w > prevBorders.z) || (activeBorders.w > prevBorders.z && activeBorders.z < prevBorders.w) || (activeBorders.z < prevBorders.z && activeBorders.w > prevBorders.w);

            Debug.Log("---");
            Debug.Log("arX: " + activeRadiusX);
            Debug.Log("arZ: " + activeRadiusZ);
            Debug.Log("prX: " + prevRadiusX);
            Debug.Log("prZ: " + prevRadiusZ);
            Debug.Log("ac: " + activeCenter);
            Debug.Log("as: " + activeSize);
            Debug.Log("pc: " + prevCenter);
            Debug.Log("ps: " + prevSize);
            Debug.Log(overlapX + " / " + overlapZ);
            Debug.Log("---");

            return overlapX && overlapZ;
        }
        return true;
    }

    public void TransformActiveToRuined()
    {
        RuinedPlateController ruined = Instantiate(ruinedPlatePrefab, activeCenter, GameManager.Manager.TowerController.transform.rotation);
        meshDimensions = activeSize;
        Debug.Log("Dim / Center: " + meshDimensions + " / " + activeCenter);
        GenerateMesh(ruined.MeshFilter);
        Destroy(activePlate.gameObject);
    }

    private void SetPlatesVariables()
    {        
        previousPlate = GameManager.Manager.TowerController.TowerPlates[^1];
        activeSize = activePlate.MeshFilter.mesh.bounds.size;
        prevSize = previousPlate.MeshFilter.mesh.bounds.size;
        prevCenter = previousPlate.transform.position;
        activeCenter = activePlate.transform.position;        
    }

}
