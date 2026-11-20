using System;
using UnityEngine;

public abstract class AudioManager : MonoBehaviour
{
    public AudioSource Audio;
    [SerializeField] private AudioClip[] _audioClips;

    protected virtual void Awake()
    {
        if (Audio == null) GetComponent<AudioSource>();
    }

    protected abstract void OnEnable();

    public virtual void PlayAudio(string clipName)
    {
        AudioClip target = Array.Find(_audioClips, sound => sound.name == clipName);
        Audio.PlayOneShot(target);
    }

    protected abstract void OnDisable();
}