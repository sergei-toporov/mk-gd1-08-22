using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    protected static ArenaManager manager;
    public static ArenaManager Manager { get => manager; }

    protected readonly Vector2Int arenaSizeLimitsDefault = new(6, 12);

    [Tooltip("A border values for the arena size in one dimension. These values will be used to get amount of rows and columns. If any value is lesser or bigger than the default, it'll be reset to one of the default limits.")]
    
    [SerializeField] protected Vector2Int arenaSizeLimits;

    protected bool hasGeneratedArena = false;

    protected ArenaRootObject arenaObject;

    protected Vector2Int arenaSizes;
    public Vector2Int ArenaSizes { get => arenaSizes; }

    [SerializeField] protected SpawnPointPlayer spawnPointPlayerPrefab;
    public SpawnPointPlayer SpawnPointPlayerPrefab { get => spawnPointPlayerPrefab; }

    [SerializeField] protected SpawnPointMonster spawnPointMonsterPrefab;
    public SpawnPointMonster SpawnPointMonsterPrefab { get => spawnPointMonsterPrefab; }

    [SerializeField] protected SpawnableMonstersCollection monsterCollection;
    public SpawnableMonstersCollection MonsterCollection { get => monsterCollection; }

    protected PlayerController player;
    public PlayerController Player { get => player; }

    [SerializeField] protected List<ArenaGeneratorBase> arenaGenerators;
    protected ArenaGeneratorBase activeGenerator;

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
        
        if (!InitialChecks())
        {
            Debug.LogError("Problems occurred during the initial check. Please, fix them.");
            Application.Quit();
        }

        InitialConfiguration();
    }

    protected bool InitialChecks()
    {
        if (arenaGenerators.Count == 0)
        {
            Debug.Log("No arena generators in the list. Add any before you can proceed.");
            return false;
        }

        return true;
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GenerateArena();
        }
    }

    protected void InitialConfiguration()
    {
        arenaSizeLimits.x = Mathf.Clamp(arenaSizeLimits.x, arenaSizeLimitsDefault.x, arenaSizeLimitsDefault.y);
        arenaSizeLimits.y = Mathf.Clamp(arenaSizeLimits.y, arenaSizeLimitsDefault.x, arenaSizeLimitsDefault.y);
        spawnPointPlayerPrefab.gameObject.SetActive(false);
        spawnPointMonsterPrefab.gameObject.SetActive(false);
    }

    protected void GenerateArena()
    {
        if (!hasGeneratedArena)
        {
            CreateRootArenaObject();
            activeGenerator = Instantiate(arenaGenerators[Random.Range(0, arenaGenerators.Count)]);
            arenaSizes = new Vector2Int (GetLimitValue(), GetLimitValue());
            activeGenerator.GenerateArena(arenaObject);
            activeGenerator.GeneratePlayerSpawnPoint();
            activeGenerator.GenerateMonsterSpawnPoints();
            StaticBatchingUtility.Combine(arenaObject.gameObject);
            
            hasGeneratedArena = true;
        }
        else
        {
            RemoveRootArenaObject();
            RemoveSpawnedCharacters();
            Destroy(activeGenerator.gameObject);
            hasGeneratedArena = false;
            GenerateArena();
        }

    }

    protected int GetLimitValue()
    {
        return Random.Range(arenaSizeLimits.x, arenaSizeLimits.y + 1);
    }

    protected void CreateRootArenaObject()
    {
        arenaObject = new GameObject(ArenaRootObject.ObjectName).AddComponent<ArenaRootObject>();
    }

    protected void RemoveRootArenaObject()
    {
        Destroy(arenaObject.gameObject);
    }

    protected void RemoveSpawnedCharacters()
    {
        foreach (SpawnableBase spawnedCharacter in FindObjectsOfType<SpawnableBase>())
        {
            Destroy(spawnedCharacter.gameObject);
        }
    }
    
    public void SetPlayerObject(SpawnPointPlayer spawnPoint)
    {
        player = FindObjectOfType<PlayerController>();
    }
}
