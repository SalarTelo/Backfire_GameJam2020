using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class BulletPickup : Pickup
    {
        [SerializeField] private BulletCase bulletCase;

        private void Awake()
        {
            Assert.IsNotNull(bulletCase.bulletComponent);

            //Apply visual
            SpriteRenderer.sprite = bulletCase.bulletComponent.Visual.Image;
            SpriteRenderer.sharedMaterial = bulletCase.bulletComponent.Visual.InstancedMaterial;
        }

        protected override bool CanPickup(IEntity entity)
        {
            InventoryComponent inventory = entity.GameObject.GetComponent<InventoryComponent>();

            return inventory != null;
        }

        protected override void OnPickup(IEntity entity)
        {
            InventoryComponent inventory = entity.GameObject.GetComponent<InventoryComponent>();

            inventory.AddBulletCase(bulletCase);
        }

    }
}