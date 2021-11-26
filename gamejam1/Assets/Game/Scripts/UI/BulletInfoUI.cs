using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;

namespace SpellcastStudios.UI
{
    public class BulletInfoUI : MonoBehaviour
    {
        public Image mainImage;
        public Image primaryColoredImage;
        public Image secondaryColoredImage;
        public Image infiniteCountImage;
        public TMP_Text countText;

        private BulletCase bulletCase;
        private int count;

        public void ClearBulletCase()
        {
            mainImage.sprite = null;

            primaryColoredImage.sprite = null;
            primaryColoredImage.color = Color.white;

            secondaryColoredImage.sprite = null;
            secondaryColoredImage.color = Color.white;

            countText.text = "";
        }

        public void SetBulletCase(BulletCase bulletCase)
        {
            this.bulletCase = bulletCase;

            if (bulletCase.IsNull)
            {
                ClearBulletCase();
                return;
            }

            Bullet bullet = bulletCase.bulletComponent;
            Assert.IsNotNull(bullet);

            mainImage.sprite = bullet.Visual.Image;
            
            primaryColoredImage.sprite = bullet.Visual.PrimaryImageMask;
            primaryColoredImage.color = bullet.Visual.PrimaryColor;

            secondaryColoredImage.sprite = bullet.Visual.SecondaryImageMask;
            secondaryColoredImage.color = bullet.Visual.SecondaryColor;

            UpdateCount();
        }

        private void Update()
        {
            if (!bulletCase.IsNull && count != bulletCase.count)
                UpdateCount();
        }

        private void UpdateCount()
        {
            count = bulletCase.count;

            if(bulletCase.count >= 0)
            {
                countText.gameObject.SetActive(true);
                countText.text = bulletCase.count.ToString();

                infiniteCountImage.gameObject.SetActive(false);
            }
            else
            {
                countText.gameObject.SetActive(false);
                infiniteCountImage.gameObject.SetActive(true);
            }
        }
    }

}