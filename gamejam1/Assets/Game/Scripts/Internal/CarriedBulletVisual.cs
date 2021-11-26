using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class CarriedBulletVisual : MonoBehaviour
    {
        [SerializeField] private InventoryComponent inventory;
        [SerializeField] private ShooterComponent shooter;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            shooter.OnCurrentBulletChange += UpdateVisual;
            inventory.OnBulletCaseChange += UpdateVisual;

            UpdateVisual();
        }

        private void OnDisable()
        {
            if(shooter != null)
                shooter.OnCurrentBulletChange += UpdateVisual;
            
            if(inventory != null)
                inventory.OnBulletCaseChange += UpdateVisual;
        }

        private void UpdateVisual()
        {
            if (shooter == null || inventory == null)
                return;

            var bulletCase = inventory.GetBulletCase(shooter.currentBullet);

            //Clear
            if(bulletCase.IsNull)
            {
                spriteRenderer.enabled = false;
                return;
            }

            spriteRenderer.sharedMaterial = bulletCase.bulletComponent.Visual.InstancedMaterial;
            spriteRenderer.sprite = bulletCase.bulletComponent.Visual.Image;
            spriteRenderer.enabled = true;

        }

    }

}