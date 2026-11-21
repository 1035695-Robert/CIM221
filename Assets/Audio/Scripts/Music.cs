using System;
using UnityEngine;

namespace Audio.Scripts
{
    public class Music : AudioManager
    {
        public static Action<string> OnMusicAudioEvent;

        protected override void OnEnable()
        {
            OnMusicAudioEvent += PlayAudio;
        }

        public override void PlayAudio(string clipName)
        {
            AudioClip song = Array.Find(_audioClips, clip => clip.name == clipName);
            if (song != null && Audio != null)
            {
                Audio.clip = song;
                Audio.loop = true;
                Audio.Play();
            }
        }

        protected override void OnDisable()
        {
            OnMusicAudioEvent -= PlayAudio;
        }
    }
}