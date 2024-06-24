using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [field: SerializeField] public AudioSource audioSource { get; private set; }
    [field: SerializeField] public List<AudioClipSetup> clips { get; private set; }


    private List<AudioSource> _persistentUsedAudioSources = new();
    private Queue<AudioSource> _FreeAudioSources = new();

    public void PlayAudio(SoundType sound)
    {
        if(_FreeAudioSources.Count == 0)
        {
            GenerateAudioSource();
        }
        AudioSource currentSource = _FreeAudioSources.Dequeue();
        try
        {
            AudioClipSetup clipSetup = clips.Find(x => x.associatedEnum == sound);
            currentSource.clip = clipSetup.GetClip();
            currentSource.loop = clipSetup.clipLoop;
            currentSource.volume = clipSetup.clipVolume;
            currentSource.Play();
            if (!clipSetup.clipLoop)
            {
                StartCoroutine(EnqueueFinishedSource(currentSource, currentSource.clip.length));
            }
            else
            {
                _persistentUsedAudioSources.Add(currentSource);
            }
        }
        catch
        {
            _FreeAudioSources.Enqueue(currentSource);
        }
    }
    public void GenerateAudioSource()
    {
        _FreeAudioSources.Enqueue(Instantiate(audioSource, Vector3.zero, Quaternion.identity, this.transform));
    }
    public IEnumerator EnqueueFinishedSource(AudioSource source, float time)
    {
        yield return new WaitForSeconds(time);
        _FreeAudioSources.Enqueue(source);
    }
}
