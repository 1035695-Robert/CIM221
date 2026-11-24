using System;
using UnityEngine;

namespace Audio.Scripts
{
    public class Sfx : AudioManager
    {
        public static Action<string> OnSfxAudioEvent;
        [SerializeField] private AudioSource HazardSource;
        [SerializeField] private AudioSource FootstepSource;
        [SerializeField] private AudioClip[] Pickup;
        [SerializeField] private AudioClip[] Drop;
        [SerializeField] private AudioClip[] ClearHazard;
        [SerializeField] private AudioClip[] Footsteps;

        protected override void OnEnable()
        {
            OnSfxAudioEvent += PlayAudio;
        }

        public override void PlayAudio(string clipName)
        {
            AudioClip selectedClip = SelectAudioClip(clipName);
            if (selectedClip == null) return;
            if (clipName == "clearHazard")
            {
                HazardSource.PlayOneShot(selectedClip);
                return;
            }

            if (clipName == "footsteps")
            {
                FootstepSource.PlayOneShot(selectedClip);
                return;
            }
            Audio.PlayOneShot(selectedClip);
        }

        private AudioClip SelectAudioClip(string clipName)
        {
            switch (clipName)
            {
                case "pickup": return Pickup[UnityEngine.Random.Range(0, Pickup.Length)];

                case "drop": return Drop[UnityEngine.Random.Range(0, Drop.Length)];

                case "clearHazard": return ClearHazard[UnityEngine.Random.Range(0, ClearHazard.Length)];

                case  "footsteps": return Footsteps[UnityEngine.Random.Range(0, Footsteps.Length)];
                default: return null;
            }
        }

        protected override void OnDisable()
        {
            OnSfxAudioEvent -= PlayAudio;
        }
    }
}