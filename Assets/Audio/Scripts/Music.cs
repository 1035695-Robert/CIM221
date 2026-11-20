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
        
        protected override void OnDisable()
        {

        }
    }
}
