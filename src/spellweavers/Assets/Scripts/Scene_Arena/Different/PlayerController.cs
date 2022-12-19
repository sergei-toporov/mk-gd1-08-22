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

    [SerializeField] protected GenericDictionary<string, PlayerFeat> feats = new GenericDictionary<string, PlayerFeat>();
    public GenericDictionary<string, PlayerFeat> Feats { get => feats; }

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        playerObject = gameObject.GetComponent<SpawnablePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Joystick.Horizontal;
        float z = Joystick.Vertical;
        Vector3 movementDirection = new Vector3(x, 0.0f, z);

        PlayerMover.SimpleMove(playerObject.CharStats.movementSpeedBase * movementDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddFeat("IncreaseBaseHealth");
            RecalculateStats();
        }
    }

    protected void TakeDamage()
    {
        playerObject.TakeDamage();
    }

    public void AddFeat(string key)
    {
        PlayerFeat feat;
        if (feats.TryGetValue(key, out feat))
        {
            feats.Remove(key);
            feat.currentLevel++;
            RecalculateFeatUpgradeCost(ref feat);
            feats.Add(key, feat);
            RecalculateStats();
            return;
        }
        
        if (ArenaManager.Manager.AvailablePlayerFeats.TryGetValue(key, out feat)) {
            feat.currentLevel++;
            RecalculateFeatUpgradeCost(ref feat);
            feats.Add(key, feat);
            RecalculateStats();
            return;
        }
    }

    /*public void RemoveFeat(PlayerFeat feat, bool forceRemoval = false)
    {
        if (feats.TryGetValue(feat, out int featLevel))
        {
            feats.Remove(feat);
            if (forceRemoval)
            {
                return;
            }

            if (featLevel > 1)
            {
                feat.currentLevel--;
                RecalculateFeatUpgradeCost(ref feat);
                feats.Add(feat, feat.currentLevel);
            }
            
        }
    }*/

    protected void RecalculateStats()
    {
        playerObject.RecalculateStats();
    }

    protected void RecalculateFeatUpgradeCost(ref PlayerFeat feat)
    {
        feat.currentImprovementCost = (feat.improvementCostBase + feat.currentLevel) + ((feat.improvementCostBase + feat.currentLevel) / 100 * feat.currentLevel);
    }

    public int GetFeatCurrentLevel(string key)
    {
        if (feats.TryGetValue(key, out PlayerFeat feat))
        {
            return feat.currentLevel;
        }

        return 0;
    }

    public PlayerFeat GetPlayerFeat(string key)
    {
        return feats.TryGetValue(key, out PlayerFeat feat) ? feat : ArenaManager.Manager.AvailablePlayerFeats[key];
    }
}
