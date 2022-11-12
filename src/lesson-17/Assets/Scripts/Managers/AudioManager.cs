using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct AudioLibrary
{
    public string key;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GenericDictionary<string, AudioClip> sfxLibrary = new GenericDictionary<string, AudioClip>();
    public GenericDictionary<string, AudioClip> SfxLibrary { get => sfxLibrary; }

    private AudioClip dumpClip;


    private static AudioManager manager;
    public static AudioManager Manager { get => manager; }

    [SerializeField] private SceneAudioController controllerPrefab;

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

        dumpClip = AudioClip.Create("DumpClip", 1, 1, 1000, false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySfxByKeyInGameObjectPosition(string key, GameObject providedGO)
    {        
        SceneAudioController audioSource = Instantiate(controllerPrefab, providedGO.transform.position, providedGO.transform.rotation);
        audioSource.PlaySound(GetSfxClipByKey(key));
               
        
    }

    public AudioClip GetSfxClipByKey(string key)
    {
        if (sfxLibrary.TryGetValue(key, out AudioClip clip))
        {
            return clip;
        }
        else
        {
            Debug.Log("The '" + key + "' clip is not in SFX collection.");
            return dumpClip;
        }
        
    }
}
