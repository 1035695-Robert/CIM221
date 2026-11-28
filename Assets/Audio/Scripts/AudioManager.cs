using System;
using UnityEngine;

public  class AudioManager : MonoBehaviour
{
    
    public AudioSource Audio;
    [SerializeField] protected AudioClip[] _audioClips;

    protected virtual void Awake()
    {
        if (Audio == null) GetComponent<AudioSource>();
    }

    protected virtual void OnEnable()
    {
        
    }

    public virtual void PlayAudio(string clipName)
    {
        AudioClip target = Array.Find(_audioClips, sound => sound.name == clipName);
        if (target == null) return;
        Audio.PlayOneShot(target);
    }

    protected virtual void OnDisable()
    {
        
    }
}