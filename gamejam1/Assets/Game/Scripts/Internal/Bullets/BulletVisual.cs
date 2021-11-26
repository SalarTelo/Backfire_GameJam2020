using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class BulletVisual : MonoBehaviour
    {
        [Header("Bullet Info")]
        [SerializeField] private Sprite image;
        [SerializeField] private Sprite primaryImageMask;
        [SerializeField] private Sprite secondaryImageMask;
        [SerializeField] private Color primaryColor;
        [SerializeField] private Color secondaryColor;

        [SerializeField] private SpriteRenderer spriteRenderer;

        public Color PrimaryColor => primaryColor;
        public Color SecondaryColor => secondaryColor;
        public Sprite Image => image;
        public Sprite PrimaryImageMask => primaryImageMask;
        public Sprite SecondaryImageMask => secondaryImageMask;

        private Material instancedMaterial;

        /// <summary>
        /// Returns material that has the correct applied color
        /// </summary>
        public Material InstancedMaterial
        {
            get
            {
                if (instancedMaterial == null)
                {
                    instancedMaterial = Material.Instantiate(spriteRenderer.sharedMaterial);
                    instancedMaterial.SetColor("_ColorPrimary", primaryColor);
                    instancedMaterial.SetColor("_ColorSecondary", secondaryColor);
                }

                return instancedMaterial;
            }
        }

        /// <summary>
        /// Sets the 
        /// </summary>
        /// <param name="primaryColor"></param>
        /// <param name="secondaryColor"></param>
        public void SetMaterialColors(Color primaryColor, Color secondaryColor)
        {
            SetPrimaryMaterialColor(primaryColor);
            SetSecondaryMaterialColor(secondaryColor);
        }

        public void SetPrimaryMaterialColor(Color primaryColor)
        {
            this.primaryColor = primaryColor;
            InstancedMaterial.SetColor("_ColorPrimary", primaryColor);
        }

        public void SetSecondaryMaterialColor(Color secondaryColor)
        {
            this.secondaryColor = secondaryColor;
            InstancedMaterial.SetColor("_ColorSecondary", secondaryColor);
        }

        public void Init(Bullet bullet)
        {
            //TODO: Make shared material use bullet's base class, which means non duplicated materials.
            //This will require the shader to allow personal values
            if (spriteRenderer != null)
                spriteRenderer.sharedMaterial = InstancedMaterial;

        }
    }
}