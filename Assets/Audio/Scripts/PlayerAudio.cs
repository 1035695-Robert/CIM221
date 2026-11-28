using System;
using UnityEngine;


namespace Audio.Scripts
{
    public class PlayerAudio : AudioManager
    {
        [SerializeField] private AudioClip[] happy;
        [SerializeField] private AudioClip[] sad;
        [SerializeField] private AudioClip[] hurt;
        [SerializeField] private AudioClip[] scared;

        public AudioClip selectedClip;
        public static Action<string> OnPlayerAudioEvent;

        protected override void OnEnable()
        {
            OnPlayerAudioEvent += PlayAudio;
        }

        public override void PlayAudio(string clipName)
        {
            selectedClip = SelectAudioClip(clipName);
            if(selectedClip == null) return;
            Audio.PlayOneShot(selectedClip);
        }

        private AudioClip SelectAudioClip(string clipName)
        {
            switch (clipName)
            {
                case "happy": return happy[UnityEngine.Random.Range(0, happy.Length)];

                case "sad": return sad[UnityEngine.Random.Range(0, sad.Length)]; 

                case "hurt": return hurt[UnityEngine.Random.Range(0, hurt.Length)];

                case "scared": return scared[UnityEngine.Random.Range(0, scared.Length)];
                default: return null;
            }
        }

        protected override void OnDisable()
        {
            OnPlayerAudioEvent -= PlayAudio;
        }
    }
}