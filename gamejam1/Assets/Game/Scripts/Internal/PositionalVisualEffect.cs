using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    /// <summary>
    /// Simple class that holds a particle, sound, and camera shake effect.
    /// </summary>
    [System.Serializable]
    public class PositionalVisualEffect
    {
        [SerializeField] private GameObject particlePrefab;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private CameraShakeSetting cameraShake;

        public void Trigger(Vector3 position)
        {
            if (particlePrefab != null)
                GameObject.Instantiate(particlePrefab, position, Quaternion.identity);

            if (cameraShake != null)
                cameraShake.ShakeAtPoint(position);

            if (audioClip != null)
                SoundManager.PlayAudioClipAtPoint(audioClip, position);
        }
    }
}