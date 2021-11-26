using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpellcastStudios
{
    public class CameraShakePoint : MonoBehaviour
    {
        public bool shakeOnStart = true;
        public float roughness = 1;
        public float magnitude = 1;
        public float distanceMultiplier = 1;
        public float fadeInTime = 0.1f;
        public float fadeOutTime = 0.1f;

        public void Start()
        {
            if (shakeOnStart)
                Shake();
        }

        public void Shake()
        {
            ShakeAtPoint(transform.position, roughness, magnitude, distanceMultiplier, fadeInTime, fadeOutTime);
        }

        public static void ShakeAtPoint(Vector3 position, float roughness, float magnitude, float distanceMultiplier, float fadeInTime = 0.1f, float fadeOutTime = 0.1f)
        {
            //Calculate distance
            float maxDistance = magnitude * distanceMultiplier;

            if (CameraManager.CurrentCamera == null)
                return;

            float distance = Vector3.Distance(CameraManager.CurrentCamera.transform.position, position);

            //If player is too far away, do nothing
            if (distance > maxDistance)
                return;

            float shakeMagnitude = magnitude / (distance / distanceMultiplier);

            if (shakeMagnitude > magnitude)
                shakeMagnitude = magnitude;

            CameraShaker shaker = CameraManager.CurrentCamera.GetComponentInChildren<CameraShaker>();
            shaker.ShakeOnce(shakeMagnitude, roughness, fadeInTime, fadeOutTime);

        }
    }

}