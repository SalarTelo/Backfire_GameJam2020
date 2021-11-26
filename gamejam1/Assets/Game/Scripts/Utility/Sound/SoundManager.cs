using NodeCanvas.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class SoundManager : MonoBehaviourSingleton<SoundManager>
    {
        private List<AudioSource> audioPool = new List<AudioSource>();

        private List<AudioSource> playingAudio = new List<AudioSource>();

        private int maxAudioPool = 30;

        /// <summary>
        /// Play a clip from a random list at a world position
        /// </summary>
        /// <param name="clips"></param>
        /// <param name="volume"></param>
        /// <param name="looped"></param>
        /// <returns></returns>
        public static AudioSource PlayAudioClipAtPoint(List<AudioClip> clips, Vector3 position, float volume = 1, bool looped = false)
        {
            if (clips.Count == 0)
                return null;

            AudioClip clip = clips[Random.Range(0, clips.Count)];
            return Instance.PlayAudioClip(clip, position, volume, looped);
        }

        /// <summary>
        /// Play a clip at a world position
        /// </summary>
        /// <param name="clips"></param>
        /// <param name="volume"></param>
        /// <param name="looped"></param>
        /// <returns></returns>
        public static AudioSource PlayAudioClipAtPoint(AudioClip clip, Vector3 position, float volume = 1, bool looped = false)
        {
            return Instance.PlayAudioClip(clip, position, volume, looped);
        }

        /// <summary>
        /// Play a clip from a random list 
        /// </summary>
        /// <param name="clips"></param>
        /// <param name="volume"></param>
        /// <param name="looped"></param>
        /// <returns></returns>
        public static AudioSource PlayAudioClip(List<AudioClip> clips, float volume = 1, bool looped = false)
        {
            if (clips.Count == 0)
                return null;

            AudioClip clip = clips[Random.Range(0, clips.Count)];
            return Instance.PlayAudioClip(clip, null, volume, looped);
        }

        /// <summary>
        /// Play a clip 
        /// </summary>
        /// <param name="clips"></param>
        /// <param name="volume"></param>
        /// <param name="looped"></param>
        /// <returns></returns>
        public static AudioSource PlayAudioClip(AudioClip clip, float volume = 1, bool looped = false)
        {
            return Instance.PlayAudioClip(clip, null, volume, looped);
        }

        /// <summary>
        /// Stops audio source and disposes into pool
        /// </summary>
        /// <param name="source"></param>
        public static void StopAudioSource(AudioSource source)
        {
            if (source == null)
                return;

            source.Stop();
            Instance.DisposeOfAudioSource(source);
        }

        private AudioSource PlayAudioClip(AudioClip clip, Vector3? position, float volume = 1,bool looped = false)
        {
            if (clip == null)
                return null;

            AudioSource source = GetAudioSource();
            source.clip = clip;
            source.loop = looped;
            source.volume = volume;
            source.dopplerLevel = 0;

            if (position == null)
            {
                source.spatialBlend = 0;
            } else
            {
                source.transform.position = (Vector3)position;
                source.spatialBlend = 1;
            }

            if (!GameManager.InstanceIsNull())
                source.outputAudioMixerGroup = GameManager.Instance.MainAudioMixerGroup;
            else
                source.outputAudioMixerGroup = null;

            source.rolloffMode = AudioRolloffMode.Linear;
            source.maxDistance = 20;

            source.Play();

            playingAudio.Add(source);

            return source;
        }

        //Every frame try to find stopped audio and dispose!
        private void Update()
        {
            for(int i = 0;i < playingAudio.Count;i++)
            {
                if (playingAudio[i] != null && playingAudio[i].isPlaying)
                    continue;

                //Remove!
                DisposeOfAudioSource(playingAudio[i]);
                i--;
            }
        }

        private void DisposeOfAudioSource(AudioSource source)
        {
            if (source == null)
                return;

            if(audioPool.Count > maxAudioPool)
            {
                Destroy(source.gameObject);
                playingAudio.Remove(source);
                return;
            }

            source.gameObject.SetActive(false);
            audioPool.Add(source);
            playingAudio.Remove(source);
        }

        private AudioSource GetAudioSource()
        {
            if (audioPool.Count == 0)
                return SpawnAudioSource();

            AudioSource source = audioPool[0];
            source.gameObject.SetActive(true);

            audioPool.RemoveAt(0);
            return source;
        }

        private AudioSource SpawnAudioSource()
        {
            GameObject audioObject = new GameObject("Audio Clip");
            audioObject.transform.parent = transform;
            AudioSource clip = audioObject.AddComponent<AudioSource>();
            clip.spatialBlend = 0;
            return clip;
        }

    }
}