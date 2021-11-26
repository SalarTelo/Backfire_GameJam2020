using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    [RequireComponent(typeof(InventoryComponent))]
    public class ShooterComponent : MonoBehaviour
    {
        private InventoryComponent inventory;

        public int currentBullet { get; private set; }
        public GameObject GameObject { get; }

        public Action OnCurrentBulletChange;

        private void Awake()
        {
            inventory = GetComponent<InventoryComponent>();
        }

        public void ShootBullet()
        {
            var bulletCase = inventory.GetBulletCase(currentBullet);

            //No bullets of type (negative count = infinite bullets)
            if (bulletCase.IsNull || bulletCase.count == 0)
                return;

            //Spawn and shoot bullet
            GameObject bulletObj = Instantiate(bulletCase.bulletPrefab);
            bulletObj.transform.position = transform.position;

            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.Init(bulletCase.bulletPrefab.GetComponent<Bullet>(), transform.right, transform, GetComponent<IEntity>());
            //Remove bullet from list
            inventory.SetBulletCount(currentBullet, bulletCase.count - 1);
        }

        public void SetCurrentBullet(int index)
        {
            currentBullet = index;
            OnCurrentBulletChange?.Invoke();
        }

    }
}