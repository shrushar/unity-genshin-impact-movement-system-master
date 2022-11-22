using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public AudioSource _audioSource;
        [field: SerializeField] public AudioClip[] JumpAudioClips { get; set; }
        [field: SerializeField] public AudioClip[] FootStepAudioClips { get; set; }

        public void Initialize()
        {
           
        }
        public void PlaySoundOnJump()
        {
            Debug.Log("Im working");

            AudioClip clipToPlay = PickRandom(JumpAudioClips);
            _audioSource.clip = clipToPlay;
            _audioSource.PlayOneShot(PickRandom(JumpAudioClips));
        }
        public void PlaySoundOnMovement()
        {

            _audioSource.PlayOneShot(PickRandom(FootStepAudioClips));
        }

        public AudioClip PickRandom(AudioClip[] clips)
        {
            return clips[UnityEngine.Random.Range(0,clips.Length-1)];
        }
    }

    
}
