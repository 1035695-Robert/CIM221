using System;
using UnityEngine;

namespace Audio.Scripts
{
    public class Sfx : AudioManager
    {
        public static Action<string> OnSfxAudioEvent;

        protected override void OnEnable()
        {
            OnSfxAudioEvent += PlayAudio;
        }

        public override void PlayAudio(string clipName)
        {
            base.PlayAudio(clipName);
            Debug.Log("Playing: " + clipName);
        }

        protected override void OnDisable()
        {
            OnSfxAudioEvent -= PlayAudio;
        }
    }
}