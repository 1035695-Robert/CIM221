using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Action<string> OnAudioEvent;

    AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        OnAudioEvent += PlayAudio;
    }

    private void PlayAudio(string clipName)
    {
        AudioClip target = Array.Find(clips, sound => sound.name == clipName);
        audioSource.PlayOneShot(target);
    }

    private void OnDisable()
    {
        OnAudioEvent -= PlayAudio;
    }
}