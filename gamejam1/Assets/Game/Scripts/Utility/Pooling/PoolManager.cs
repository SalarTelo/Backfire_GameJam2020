using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public class PoolManager : MonoBehaviourSingleton<PoolManager>
    {
        public bool logStatus;
        private Transform root;

        private Dictionary<UnityEngine.Object, ObjectPool<UnityEngine.Object>> prefabLookup;
        private Dictionary<UnityEngine.Object, ObjectPool<UnityEngine.Object>> instanceLookup;

        public PoolManager()
        {
            prefabLookup = new Dictionary<UnityEngine.Object, ObjectPool<UnityEngine.Object>>();
            instanceLookup = new Dictionary<UnityEngine.Object, ObjectPool<UnityEngine.Object>>();

        }

        public Dictionary<UnityEngine.Object, ObjectPool<UnityEngine.Object>> GetPrefabLookup()
        {
            return prefabLookup;
        }

        public Dictionary<UnityEngine.Object, ObjectPool<UnityEngine.Object>> GetInstanceLookup()
        {
            return instanceLookup;
        }

        public void _WarmPool(UnityEngine.Object prefab, int size)
        {
            if (prefabLookup.ContainsKey(prefab))
            {
                return;
            }


            var pool = new ObjectPool<UnityEngine.Object>(() => { return InstantiatePrefab(prefab); }, size);
            prefabLookup[prefab] = pool;

        }

        public UnityEngine.Object _CreateObject(UnityEngine.Object prefab)
        {
            if (!prefabLookup.ContainsKey(prefab))
            {

                WarmPool(prefab, 1);
            }

            var pool = prefabLookup[prefab];

            UnityEngine.Object clone = pool.GetItem();

            instanceLookup.Add(clone, pool);

            return clone;
        }

        public GameObject _SpawnGameObject(GameObject prefab)
        {
            return _SpawnGameObject(prefab, Vector3.zero, Quaternion.identity);
        }

        public GameObject _SpawnGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!prefabLookup.ContainsKey(prefab))
            {

                WarmPool(prefab, 1);
            }

            var pool = prefabLookup[prefab];

            GameObject clone = pool.GetItem() as GameObject;
            clone.transform.position = position;
            clone.transform.rotation = rotation;
            clone.SetActive(true);

            instanceLookup.Add(clone, pool);

            return clone;
        }

        public void _ReleaseObject(UnityEngine.Object clone)
        {
            if (clone == null)
                return;

            GameObject gameObj = clone as GameObject;

            if (gameObj != null)
            {
                gameObj.transform.SetParent(transform);
                gameObj.SetActive(false);
            }

            if (instanceLookup.ContainsKey(clone))
            {
                instanceLookup[clone].ReleaseItem(clone);
                instanceLookup.Remove(clone);

            }
            else
            {
                Debug.LogWarning("No pool contains the object: " + clone.name + " - " + clone.GetType());
            }
        }

        private UnityEngine.Object InstantiatePrefab(UnityEngine.Object prefab)
        {
            UnityEngine.Object go = null;

            go = GameObject.Instantiate(prefab, transform);

            return go;
        }

        #region Static API

        public static void WarmPool(UnityEngine.Object prefab, int size)
        {
            Instance._WarmPool((UnityEngine.Object)prefab, size);
        }

        public static UnityEngine.Object CreateObject(UnityEngine.Object prefab)
        {
            return Instance._CreateObject(prefab);
        }

        public static GameObject SpawnGameObject(GameObject prefab)
        {
            return Instance._SpawnGameObject(prefab);
        }

        public static GameObject SpawnGameObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instance._SpawnGameObject(prefab, position, rotation);
        }

        public static void ReleaseObject(UnityEngine.Object clone)
        {
            Instance._ReleaseObject(clone);
        }

        #endregion
    }



}
