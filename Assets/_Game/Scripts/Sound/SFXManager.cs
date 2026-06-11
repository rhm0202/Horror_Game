using UnityEngine;
using System.Collections.Generic;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance { get; private set; }

    [System.Serializable]
    public class SoundEntry
    {
        public string key;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }

    [SerializeField] private SoundEntry[] soundList;
    private Dictionary<string, SoundEntry> soundDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        soundDict = new Dictionary<string, SoundEntry>();
        foreach (var entry in soundList)
            soundDict[entry.key] = entry;
    }

    public void PlaySFX(string key)
    {
        if (!soundDict.TryGetValue(key, out SoundEntry entry))
        {
            Debug.LogWarning($"[SFXManager] '{key}' 키를 찾을 수 없습니다.");
            return;
        }

        GameObject tempObj = new GameObject($"SFX_{key}");
        AudioSource source = tempObj.AddComponent<AudioSource>();
        source.clip = entry.clip;
        source.volume = entry.volume;
        source.Play();

        Destroy(tempObj, entry.clip.length);
    }
}