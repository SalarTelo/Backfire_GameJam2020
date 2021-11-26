using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    [CreateAssetMenu(menuName ="Spellcast/CameraShakeSetting")]
    public class CameraShakeSetting : ScriptableObject
    {
        public float roughness;
        public float magnitude;
        public float distanceMult;
        public float fadeIn = 0.1f;
        public float fadeOut = 0.1f;
        public float directionalOffset;
        public float soundVolume = 1;
        public List<GameObject> particlePrefabs;

        public void ShakeAtPoint(Vector3 point)
        {
            CameraShakePoint.ShakeAtPoint(point, roughness, magnitude, distanceMult, fadeIn, fadeOut);

            if (particlePrefabs.Count != 0)
            {
                int ran = UnityEngine.Random.Range(0, particlePrefabs.Count);
                GameObject particleObj = GameObject.Instantiate(particlePrefabs[ran]);
                particleObj.transform.position = point;
            }
        }

        public void ShakeAtPoint(Vector3 point,Vector3 direction)
        {
            ShakeAtPoint(point + direction * directionalOffset);
        }
    }

}
