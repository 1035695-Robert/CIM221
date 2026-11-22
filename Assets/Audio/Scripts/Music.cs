using System;
using UnityEngine;

namespace Audio.Scripts
{
    public class Music : AudioManager
    {
        public static Action<string> OnMusicAudioEvent;
        [SerializeField] private AudioClip[] Base;
        [SerializeField] private AudioClip[] panic;
        [SerializeField] private AudioClip[] Tense;
        
        [SerializeField] private AudioSource AudioB;
        private bool isUsingAudioA;


        private void Start()
        {
            PreloadCLips(Base);
            PreloadCLips(panic);
            PreloadCLips(Tense);
        }

        private void PreloadCLips(AudioClip[] clips)
        {
            if (clips == null) return;
            foreach (var clip in clips)
            {
                if (clip != null && clip.loadState != AudioDataLoadState.Loaded)
                {
                    clip.LoadAudioData();
                }
            }
        }

        protected override void OnEnable()
        {
          
            OnMusicAudioEvent += PlayAudio;
        }

        public override void PlayAudio(string clipName)
        {
           AudioClip nextSong = SelectAudioClip(clipName);
           if (nextSong == null) return;
          
           AudioSource activeSource = isUsingAudioA ? Audio : AudioB;
           AudioSource NextSource = isUsingAudioA ? AudioB : Audio;
           
           if(activeSource.clip == nextSong && activeSource.isPlaying) return;
           double switchTime = AudioSettings.dspTime + 0.05f;

           if (activeSource.isPlaying)
           {
               int sampleToAdvance = (int)(0.05 * activeSource.clip.frequency);
               int targetSample = (activeSource.timeSamples + sampleToAdvance) % nextSong.samples;
               
               NextSource.clip = nextSong;
               NextSource.timeSamples = targetSample;
               
               activeSource.SetScheduledEndTime(switchTime);
               NextSource.PlayScheduled(switchTime);
           }
           else
           {
               NextSource.clip = nextSong;
               NextSource.timeSamples = 0;
               NextSource.Play();
           }
           
           isUsingAudioA = !isUsingAudioA;
        }
        

        private AudioClip SelectAudioClip(string clipName)
        {
            switch (clipName)
            {
                case "Base": return Base[UnityEngine.Random.Range(0, Base.Length)];

                case "Panic": return panic[UnityEngine.Random.Range(0, panic.Length)];

                case "Tense": return Tense[UnityEngine.Random.Range(0, Tense.Length)];
                
                default: return null;
            }
        }

        protected override void OnDisable()
        {
            OnMusicAudioEvent -= PlayAudio;
        }
    }
}