using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class SpawnTrailModifier : BulletModifier
    {
        [SerializeField] private float startDelay;
        [SerializeField] private float delay;
        [SerializeField] private bool addLifetime;
        [SerializeField] private float lifetime;

        [SerializeField] List<GameObject> spawnPrefabList;

        private float startTime;
        private float lastSpawnTime;

        private void Awake()
        {
            startTime = Time.time;
        }

        private void Update()
        {
            if (bullet.IsDestroyed)
                return;

            if (spawnPrefabList.Count == 0)
                return;

            //Don't start spawning yet
            if (startTime + startDelay > Time.time)
                return;

            //Time to spawn!
            if (lastSpawnTime + delay < Time.time)
            {
                int index = Random.Range(0, spawnPrefabList.Count);

                GameObject spawnedObject = Instantiate(spawnPrefabList[index]);
                spawnedObject.transform.position = transform.position;

                if (addLifetime)
                    spawnedObject.AddComponent<TimedObjectDestructor>().timeOut = lifetime;

                lastSpawnTime = Time.time;
            }
        }
    }

}