using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClip", menuName = "DucksSelection/AudioClip")]
public class AudioClipSetup : ScriptableObject
{
    [field: SerializeField] public List<AudioClip> clipList { get; private set; }
    [field: SerializeField] public bool clipLoop { get; private set; }
    [field: SerializeField] public float clipVolume { get; private set; }
    [field: SerializeField] public SoundType associatedEnum { get; private set; }


    public AudioClip GetClip()
    {
        return clipList[Random.Range(0, clipList.Count)];
    }
}
