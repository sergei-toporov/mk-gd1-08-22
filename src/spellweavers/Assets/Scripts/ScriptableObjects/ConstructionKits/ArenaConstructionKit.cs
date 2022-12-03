using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_arena_construction_kit", menuName = "Custom Assets/Construction Kit", order = 51)]
public class ArenaConstructionKit : ScriptableObject
{
    [SerializeField] protected List<Transform> walls;
    public List<Transform> Walls { get => walls; }

    [SerializeField] protected List<Transform> floors;
    public List<Transform> Floors { get => floors; }

    [SerializeField] protected List<Transform> obstacles;
    public List<Transform> Obstacles { get => obstacles; }
}
