using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ardenfall
{
    //Attach this to a particle system - when the system is finished, it will automatically 
    //Destroy itself
    public class AutoParticleDestroy : MonoBehaviour
    {
        public GameObject destroyCustom;

        private IEnumerator Start()
        {
            ParticleSystem sys = GetComponent<ParticleSystem>();

            yield return new WaitForSeconds(sys.main.duration + sys.main.startLifetimeMultiplier);

            if (destroyCustom != null)
                Destroy(destroyCustom);
            else
                Destroy(gameObject);
        }

    }

}
