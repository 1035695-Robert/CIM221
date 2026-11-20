using System;
using UnityEngine;


namespace Audio.Scripts
{
    public class UiAudio : AudioManager
    {
        public static Action<string> OnUIAudioEvent;

        protected override void OnEnable()
        {
            OnUIAudioEvent += PlayAudio;
        }

        protected override void OnDisable()
        {
            OnUIAudioEvent -= PlayAudio;
        }
    }
}
