using UnityEngine;
using System;

namespace SpellcastStudios
{
    /// <summary>
    /// A monobehavior "singleton" that only exists in the world instance.
    /// it does NOT persist if the world is reloaded.
    /// There is currently nothing stopping multiple singletons of the same type
    /// from existing. This is something that will need to be fixed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WorldSingleton<T> : MonoBehaviour where T : WorldSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (!Application.isPlaying)
                {
                    Debug.LogWarning("Trying to access WorldSingleton Instance during Edit Mode is not allowed");
                    return null;
                }

                if (instance == null)
                {
                    if (isQuitting)
                    {
                        Debug.LogWarning("Trying to access WorldSingleton on application may cause issues.");
                        return null;
                    }

                    GameObject instanceObj = new GameObject();
                    instanceObj.name = typeof(T).Name;
                    instance = (T)instanceObj.AddComponent(typeof(T));
                    //instance = (T)Activator.CreateInstance(typeof(T));
                }

                return instance;
            }
            set
            {
                if (!Application.isPlaying)
                {
                    Debug.LogWarning("Trying to set WorldSingleton Instance during Edit Mode is not allowed");
                    return;
                }

                instance = value as T;
            }
        }

        protected static T instance;
        public static bool isQuitting;

        public static bool InstanceIsNull()
        {
            return instance == null;
        }

        private void OnApplicationQuit()
        {
            isQuitting = true;
        }
    }

}
