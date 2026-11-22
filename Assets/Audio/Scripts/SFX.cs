using System;
using UnityEngine;

namespace Audio.Scripts
{
    public class Sfx : AudioManager
    {
        public static Action<string> OnSfxAudioEvent;
        [SerializeField] private AudioClip[] Pickup;
        [SerializeField] private AudioClip[] Drop;
        [SerializeField] private AudioClip[] ClearHazard;


        protected override void OnEnable()
        {
            OnSfxAudioEvent += PlayAudio;
        }

        public override void PlayAudio(string clipName)
        {
            AudioClip selectedClip = SelectAudioClip(clipName);
            if (selectedClip == null) return;
            Audio.PlayOneShot(selectedClip);
        }

        private AudioClip SelectAudioClip(string clipName)
        {
            switch (clipName)
            {
                case "pickup": return Pickup[UnityEngine.Random.Range(0, Pickup.Length)];

                case "drop": return Drop[UnityEngine.Random.Range(0, Drop.Length)];

                case "clearHazard": return ClearHazard[UnityEngine.Random.Range(0, ClearHazard.Length)];

                default: return null;
            }
        }

        protected override void OnDisable()
        {
            OnSfxAudioEvent -= PlayAudio;
        }
    }
}