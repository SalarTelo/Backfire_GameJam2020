using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

namespace SpellcastStudios.UI
{
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        [SerializeField] private Sprite fillHalf;
        [SerializeField] private Sprite fillFull;

        /// <summary>
        /// Sets the fill of the heart. 0 = empty, 1 = half, 2 = full
        /// </summary>
        /// <param name="fill"></param>
        public void SetFill(int fill)
        {
            if (fill == 0)
            {
                fillImage.sprite = null;
                fillImage.enabled = false;
            }

            else if (fill == 1)
            {
                fillImage.sprite = fillHalf;
                fillImage.enabled = true;
            }

            else if (fill == 2)
            {
                fillImage.sprite = fillFull;
                fillImage.enabled = true;
            }

            else
                Debug.LogError("Fill Amount must be between 0-2 inclusive");
        }
    }
}