using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpellcastStudios
{

    public class CoroutineManager : MonoBehaviour
    {
        [ClearOnReload] private static CoroutineManager _instance;
        [ClearOnReload] private static bool isQuitting;
        [ClearOnReload] private static Action UpdateLoop;

        private static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (isQuitting)
                    {
                        Debug.LogError("Trying to generate Coroutine Instance during Application Quit");
                        return null;
                    }

                    GameObject obj = new GameObject();
                    obj.name = "__CoroutineManager";
                    _instance = obj.AddComponent<CoroutineManager>();
                }
                return _instance;
            }
        }

        private void Update()
        {
            UpdateLoop?.Invoke();
        }

        private void OnApplicationQuit()
        {
            isQuitting = true;
        }

        public static void RegisterUpdateLoop(Action action)
        {
            UpdateLoop += action;
        }

        public static void UnregisterUpdateLoop(Action action)
        {
            UpdateLoop -= action;
        }

        public static void StopACoroutine(Coroutine coroutine)
        {
            if (coroutine != null && !isQuitting)
                Instance.StopCoroutine(coroutine);
        }

        public static Coroutine RunExternalCoroutine(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }

        public static Coroutine RunDelayingCoroutine(float delay, Action onComplete, bool unscaled = false)
        {
            IEnumerator coroutine = Instance.DelayingCoroutine(delay, unscaled, onComplete);
            return Instance.StartCoroutine(coroutine);
        }

        public static Coroutine RunFrameCoroutine(Action onComplete)
        {
            return Instance.StartCoroutine("FrameCoroutine", onComplete);
        }

        private IEnumerator FrameCoroutine(Action onComplete)
        {
            //Wait for one frame
            yield return null;

            if (onComplete != null)
                onComplete();
        }

        private IEnumerator DelayingCoroutine(float delay, bool unscaled, Action onComplete)
        {
            if (unscaled)
                yield return new WaitForSecondsRealtime(delay);
            else
                yield return new WaitForSeconds(delay);

            if (onComplete != null)
                onComplete();
        }

    }

}
