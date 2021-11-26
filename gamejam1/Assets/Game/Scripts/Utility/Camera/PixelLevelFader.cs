using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class PixelLevelFader : MonoBehaviour
    {
        public float ppuMultiplier = 1;
        private UnityEngine.U2D.PixelPerfectCamera pixelPerfectCamera;

        private void Start()
        {
            pixelPerfectCamera = GetComponent<UnityEngine.U2D.PixelPerfectCamera>();

            //Register
            GameManager.Instance.OnLoadFadeStart += OnFade;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnLoadFadeStart -= OnFade;
        }

        private void OnFade(float delay)
        {
            StartCoroutine(FadeOutPixels(delay));
        }

        IEnumerator FadeOutPixels(float delay)
        {
            float startTime = Time.time;
            Vector2Int startPixels = new Vector2Int(pixelPerfectCamera.refResolutionX, pixelPerfectCamera.refResolutionY);
            int startUnit = pixelPerfectCamera.assetsPPU;

            while (startTime + delay > Time.time)
            {
                float ratio = (startTime + delay - Time.time) / delay;

                if (ratio <= 0.05f)
                    ratio = 0.05f;

                if(ppuMultiplier != 0)
                    pixelPerfectCamera.assetsPPU = pixelPerfectCamera.assetsPPU + Mathf.RoundToInt(ratio * ppuMultiplier);

                pixelPerfectCamera.refResolutionX = Mathf.Clamp(Mathf.RoundToInt((float)startPixels.x * ratio), 2, startPixels.x);
                pixelPerfectCamera.refResolutionY = Mathf.Clamp(Mathf.RoundToInt((float)startPixels.y * ratio), 2, startPixels.y);

                yield return null;
            }
        }


    }

}