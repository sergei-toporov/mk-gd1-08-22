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

    protected float attackDelayTime;
    protected WaitForSeconds attackDelayObject;

    protected bool canAttack = false;
    protected bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        playerObject = gameObject.GetComponent<SpawnablePlayer>();
        onCharacterEmitter = GetComponentInChildren<OnCharacterEmitterController>();
        if (playerObject.CharStats.attacksPerMinute > 0.0f) {
            SetAttackDelayParameters();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x = Joystick.Horizontal;
        float z = Joystick.Vertical;
        Vector3 movementDirection = new Vector3(x, 0.0f, z);
        Vector3 lookAtDirection = playerObject.transform.position + movementDirection;
        movementDirection.y -= 9.81f;

        playerObject.transform.LookAt(lookAtDirection);
        PlayerMover.SimpleMove(playerObject.CharStats.movementSpeedBase * movementDirection);

        if (canAttack && !isAttacking) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
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
        isAttacking = true;
        if (playerObject.HitterPrefab != null)
        {
            WeaponHitter strike = Instantiate(playerObject.HitterPrefab, onCharacterEmitter.transform.position, onCharacterEmitter.transform.rotation);
            strike.SetParent(playerObject);
            if (strike.TryGetComponent(out Rigidbody strikeRb))
            {
                strikeRb.AddForce(onCharacterEmitter.transform.forward, ForceMode.Impulse);
            }
        }
        StartCoroutine(AttackDelay());
    }

    protected void SetAttackDelayParameters()
    {
        attackDelayTime = 60.0f / playerObject.CharStats.attacksPerMinute;
        canAttack = true;
        attackDelayObject = new WaitForSeconds(attackDelayTime);
    }

    protected IEnumerator AttackDelay()
    {
        yield return attackDelayObject;
        isAttacking = false;
    }
}
