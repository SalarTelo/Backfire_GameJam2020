using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    /// <summary>
    /// Disconnects attached gameobject, and then follows the old parent
    /// </summary>
    public class DisconnectAndFollow : MonoBehaviour
    {
        private Transform oldParent;
        private ParticleSystem particleSystem;

        public bool stopParticlesOnLoseParent;
        public bool destroyTimerOnLoseParent;
        public float destroyTimer;

        private bool addedTimer;

        private void Start()
        {
            oldParent = transform.parent;
            transform.parent = null;

            particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (oldParent != null)
                transform.position = oldParent.position;
            
            else
            {
                //Stop particle system
                if (particleSystem != null && particleSystem.isPlaying && stopParticlesOnLoseParent)
                    particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);

                //Add destroy timer
                if(destroyTimerOnLoseParent && !addedTimer)
                {
                    TimedObjectDestructor destructor = gameObject.AddComponent<TimedObjectDestructor>();
                    destructor.timeOut = destroyTimer;
                    addedTimer = true;
                }
            }
            

        }
    }

}