using UnityEngine;
using System.Collections;

namespace SpellcastStudios
{
    public class TimedObjectDestructor : MonoBehaviour
    {

        public float timeOut = 1.0f;
        public bool onlyDisable = false;
        public bool detachChildren = false;
        public GameObject spawnOnDestroy;

        void Start()
        {
            StartCoroutine("_DestroyNow");
        }

        IEnumerator _DestroyNow()
        {
            yield return new WaitForSeconds(timeOut);

            if (detachChildren)
                transform.DetachChildren();

            if(spawnOnDestroy != null)
            {
                GameObject spawned = GameObject.Instantiate(spawnOnDestroy);
                spawned.transform.position = transform.position;
                spawned.transform.rotation = transform.rotation;
            }

            if (onlyDisable)
                this.gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
    }

}
