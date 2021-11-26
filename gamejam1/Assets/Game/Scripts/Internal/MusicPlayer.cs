using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip song;

        [SerializeField] private float volume = 1;

        private AudioSource source;
        private void Awake()
        {
             source = SoundManager.PlayAudioClip(song, volume, true);
        }

        private void OnDestroy()
        {
            SoundManager.StopAudioSource(source);
        }
    }
}