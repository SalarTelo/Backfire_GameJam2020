using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public class PoolOnLoad : MonoBehaviour
    {
        [System.Serializable]
        public struct PoolObject
        {
            public GameObject gameObject;
            public int count;
        }

        public List<PoolObject> objectsToPool;

        void Start()
        {
            for (int i = 0; i < objectsToPool.Count; i++)
            {
                PoolManager.WarmPool((Object)objectsToPool[i].gameObject, objectsToPool[i].count);
            }
        }
    }
}
