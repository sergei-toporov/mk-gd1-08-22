using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "new_scenes_list", menuName = "Custom Assets/Scenes Management/Scenes list", order = 53)]
public class ScenesListSO : ScriptableObject
{
    [SerializeField] protected GenericDictionary<string, SceneAsset> scenesList = new GenericDictionary<string, SceneAsset>();
    public GenericDictionary<string, SceneAsset> ScenesList { get => scenesList; }

    public string GetSceneNameByKey(string key)
    {
        if (scenesList.TryGetValue(key, out SceneAsset scene))
        {
            return scene.name;
        }

        return null;
    }
}
