using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpellcastStudios
{
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> clips;
        [SerializeField] private bool loop;
        [SerializeField] private bool stopLoopOnDestroy = true;

        private AudioSource source;

        private void Start()
        {
            source = SoundManager.PlayAudioClipAtPoint(clips, transform.position,1, loop);
        }

        private void Update()
        {
            if (loop)
                source.transform.position = transform.position;
        }

        private void OnDestroy()
        {
            if (stopLoopOnDestroy && SoundManager.Instance != null)
                SoundManager.StopAudioSource(source);
        }
    }

}