using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected Joystick joystick;
    public Joystick Joystick { get => joystick; }

    protected CharacterController playerMover;
    public CharacterController PlayerMover { get => playerMover ?? GetComponent<CharacterController>(); }

    protected SpawnablePlayer playerObject;

    [SerializeField] protected GenericDictionary<string, PlayerAbility> abilities = new GenericDictionary<string, PlayerAbility>();
    public GenericDictionary<string, PlayerAbility> Abilities { get => abilities; }

    [SerializeField] protected OnCharacterEmitterController onCharacterEmitter;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        playerObject = gameObject.GetComponent<SpawnablePlayer>();
        onCharacterEmitter = GetComponentInChildren<OnCharacterEmitterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Joystick.Horizontal;
        float z = Joystick.Vertical;
        Vector3 movementDirection = new Vector3(x, 0.0f, z);
        Vector3 lookAtDirection = playerObject.transform.position + movementDirection;

        playerObject.transform.LookAt(lookAtDirection);
        PlayerMover.SimpleMove(playerObject.CharStats.movementSpeedBase * movementDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public void AddAbility(string key)
    {
        PlayerAbility ability;
        if (abilities.TryGetValue(key, out ability))
        {
            abilities.Remove(key);
            ability.currentLevel++;
            RecalculateFeatUpgradeCost(ref ability);
            abilities.Add(key, ability);
            RecalculateStats();
            return;
        }
        
        if (ArenaResourceManager.Manager.AvailablePlayerAbilities.TryGetValue(key, out ability)) {
            ability.currentLevel++;
            RecalculateFeatUpgradeCost(ref ability);
            abilities.Add(key, ability);
            RecalculateStats();
            return;
        }
    }

    protected void RecalculateStats()
    {
        playerObject.RecalculateStats();
    }

    protected void RecalculateFeatUpgradeCost(ref PlayerAbility ability)
    {
        ability.currentImprovementCost = (ability.improvementCostBase + ability.currentLevel) + ((ability.improvementCostBase + ability.currentLevel) / 100 * ability.currentLevel);
    }

    public int GetFeatCurrentLevel(string key)
    {
        if (abilities.TryGetValue(key, out PlayerAbility feat))
        {
            return feat.currentLevel;
        }

        return 0;
    }

    public PlayerAbility GetPlayerAbility(string key)
    {
        return abilities.TryGetValue(key, out PlayerAbility ability) ? ability : ArenaResourceManager.Manager.AvailablePlayerAbilities[key];
    }

    public void Attack()
    {
        if (playerObject.HitterPrefab != null)
        {
            WeaponHitter strike = Instantiate(playerObject.HitterPrefab, onCharacterEmitter.transform.position, onCharacterEmitter.transform.rotation);
            strike.SetParent(playerObject);
            if (strike.TryGetComponent(out Rigidbody strikeRb))
            {
                strikeRb.AddForce(onCharacterEmitter.transform.forward, ForceMode.Impulse);
            }
        }
    }
}
