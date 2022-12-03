using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    protected static ArenaManager manager;
    public static ArenaManager Manager { get => manager; }

    protected readonly Vector2Int arenaSizeLimitsDefault = new(4, 10);

    [Tooltip("A border values for the arena size in one dimension. These values will be used to get amount of rows and columns. If any value is lesser or bigger than the default, it'll be reset to one of the default limits.")]
    
    [SerializeField] protected Vector2Int arenaSizeLimits;

    [SerializeField] protected List<ArenaConstructionKit> constructionKits;

    protected bool hasGeneratedArena = false;

    protected GameObject arenaObject;

    protected Vector2Int arenaSizes;

    protected Vector3 floorMeshSize;

    protected Vector3 wallMeshSize;

    protected Vector2 arenaDimensions;

    protected float wallPadding = 2.0f;

    [SerializeField] protected SpawnPointPlayer spawnPointPlayerPrefab;
    [SerializeField] protected SpawnPointMonster spawnPointMonsterPrefab;
    [SerializeField] protected SpawnableMonstersCollection monsterCollection;
    public SpawnableMonstersCollection MonsterCollection { get => monsterCollection; }

    protected PlayerController player;
    public PlayerController Player { get => player; }


    protected void Awake()
    {
        if (manager != null && manager != this)
        {
            Destroy(this);
        }
        else
        {
            manager = this;
        }

        InitialConfiguration();
    }


    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateArena();
            GenerateMonsterSpawnPoints();
            GeneratePlayerSpawnPoint();
        }
    }

    protected void InitialConfiguration()
    {
        arenaSizeLimits.x = Mathf.Clamp(arenaSizeLimits.x, arenaSizeLimitsDefault.x, arenaSizeLimitsDefault.y);
        arenaSizeLimits.y = Mathf.Clamp(arenaSizeLimits.y, arenaSizeLimitsDefault.x, arenaSizeLimitsDefault.y);
    }

    protected void GeneratePlayerSpawnPoint()
    {
        SpawnPointPlayer point = Instantiate(spawnPointPlayerPrefab, Vector3.zero, Quaternion.identity);
        Collider pCollider = point.GetComponent<Collider>();
        Vector3 colSize = pCollider.bounds.size;
        Vector3 pos = new(
            arenaDimensions.x / 2,
            colSize.y / 2,
            arenaDimensions.y / 2
            );
        point.transform.position = pos;
        point.transform.parent = arenaObject.transform;

        player = FindObjectOfType<PlayerController>();
    }

    protected void GenerateMonsterSpawnPoints()
    {
        // For now ...
        SpawnPointMonster point = Instantiate(spawnPointMonsterPrefab, Vector3.zero, Quaternion.identity);
        Collider pCollider = point.GetComponent<Collider>();
        Vector3 colSize = pCollider.bounds.size;
        Vector3 pos = new(
            0.0f,
            colSize.y / 2,
            arenaDimensions.y / 2
            );
        point.transform.position = pos;
        point.transform.parent = arenaObject.transform;

        //
        point = Instantiate(spawnPointMonsterPrefab, Vector3.zero, Quaternion.identity);
        pCollider = point.GetComponent<Collider>();
        colSize = pCollider.bounds.size;
        pos = new(
            arenaDimensions.x - 2 * wallPadding,
            colSize.y / 2,
            arenaDimensions.y / 2
            );
        point.transform.position = pos;
        point.transform.parent = arenaObject.transform;

        //
        point = Instantiate(spawnPointMonsterPrefab, Vector3.zero, Quaternion.identity);
        pCollider = point.GetComponent<Collider>();
        colSize = pCollider.bounds.size;
        pos = new(
            arenaDimensions.x / 2,
            colSize.y / 2,
            0.0f
            );
        point.transform.position = pos;
        point.transform.parent = arenaObject.transform;

        //
        point = Instantiate(spawnPointMonsterPrefab, Vector3.zero, Quaternion.identity);
        pCollider = point.GetComponent<Collider>();
        colSize = pCollider.bounds.size;
        pos = new(
            arenaDimensions.x / 2,
            colSize.y / 2,
            arenaDimensions.y - 2 * wallPadding
            );
        point.transform.position = pos;
        point.transform.parent = arenaObject.transform;

    }

    /**
     * This method should be moved into the separate generator class.
     * And refactored of course.
     */
    protected void GenerateArena()
    {
        if (!hasGeneratedArena)
        {
            arenaSizes = new(
                GetLimitValue(),
                GetLimitValue()
                );
            //arenaConstructionComponents = new Transform[100];
            Transform[] floors = new Transform[arenaSizes.x * arenaSizes.y];

            ArenaConstructionKit kit = constructionKits[Random.Range(0, constructionKits.Count)];

            for (int i = 0; i < floors.Length; i++)
            {
                floors[i] = kit.Floors[Random.Range(0, kit.Floors.Count)];
            }

            int wallsAmount = 2 * arenaSizes.x + 2 * arenaSizes.y;
            Transform[] walls = new Transform[wallsAmount];

            for (int i = 0; i < wallsAmount; i++)
            {
                walls[i] = kit.Walls[Random.Range(0, kit.Walls.Count)];
            }

            arenaObject = new GameObject("ArenaStructure");
            arenaObject.transform.position = Vector3.zero;

            MeshFilter floorMesh = floors[0].GetComponent<MeshFilter>();
            floorMeshSize = floorMesh.sharedMesh.bounds.size;
            arenaDimensions = new(
                floorMeshSize.x * arenaSizes.x,
                floorMeshSize.z * arenaSizes.y
                );


            for (int row = 0; row < arenaSizes.x; row++)
            {
                for (int col = 0; col < arenaSizes.y; col++)
                {
                    Vector3 pos = new(row * floorMeshSize.x, 0.0f, col * floorMeshSize.z) ;
                    Transform arenaComponent = Instantiate(floors[row + col], pos, Quaternion.identity);
                    arenaComponent.parent = arenaObject.transform;
                }
            }

            MeshFilter wallMesh = walls[0].GetComponent<MeshFilter>();
            wallMeshSize = wallMesh.sharedMesh.bounds.size;

            Vector3 wallCoord = new(
                (arenaSizes.x * floorMeshSize.x) - (floorMeshSize.x / 2),
                wallMeshSize.y / 2,
                -floorMeshSize.z / 2
                );

            for (int i = 0; i < arenaSizes.x; i++)
            {
                Transform arenaComponent = Instantiate(walls[Random.Range(0, walls.Length)], wallCoord, Quaternion.identity);
                wallCoord.x -= floorMeshSize.x;
                arenaComponent.parent = arenaObject.transform;
            }

            //wallCoord.x -= meshSize.x;

            for (int i = 0; i < arenaSizes.y; i++)
            {
                Transform arenaComponent = Instantiate(walls[Random.Range(0, walls.Length)], wallCoord, Quaternion.Euler(0.0f, 90.0f, 0.0f));
                wallCoord.z += floorMeshSize.z;
                arenaComponent.parent = arenaObject.transform;
            }

            for (int i = 0; i < arenaSizes.x; i++)
            {
                Transform arenaComponent = Instantiate(walls[Random.Range(0, walls.Length)], wallCoord, Quaternion.Euler(0.0f, 180.0f, 0.0f));
                wallCoord.x += floorMeshSize.x;
                arenaComponent.parent = arenaObject.transform;
            }

            //wallCoord.x -= meshSize.x;

            for (int i = 0; i < arenaSizes.y; i++)
            {
                Transform arenaComponent = Instantiate(walls[Random.Range(0, walls.Length)], wallCoord, Quaternion.Euler(0.0f, 270.0f, 0.0f));
                wallCoord.z -= floorMeshSize.z;
                arenaComponent.parent = arenaObject.transform;
            }

            int amountOfObstacles = Random.Range(arenaSizes.x, arenaSizes.y + 1);

            for (int i = 0; i < amountOfObstacles; i++)
            {
                Transform obstacle = Instantiate(kit.Obstacles[Random.Range(0, kit.Obstacles.Count)], GetRandomPointOnArena(), Quaternion.identity);
                MeshFilter obstacleMF = obstacle.GetComponent<MeshFilter>();
                obstacle.transform.parent = arenaObject.transform;
                obstacle.transform.position += new Vector3(0.0f, obstacleMF.sharedMesh.bounds.size.y / 2, 0.0f);
            }

            StaticBatchingUtility.Combine(arenaObject);


            hasGeneratedArena = true;
        }
        else
        {
            Destroy(arenaObject);
            hasGeneratedArena = false;
            GenerateArena();
        }

    }

    protected int GetLimitValue()
    {
        return Random.Range(arenaSizeLimits.x, arenaSizeLimits.y + 1);
    }

    protected Vector3 GetRandomPointOnArena()
    {
        float x = arenaDimensions.x - wallPadding;
        float y = arenaDimensions.y - wallPadding;


        return new Vector3(
            Random.Range(wallPadding, x),
            0.0f,
            Random.Range(wallPadding, y)
            );
    }

}
