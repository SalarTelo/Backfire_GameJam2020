using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class CreateNewBulletOnDestroyModifier : BulletModifier
    {

        //TODO: Everything -Loran
        [Range(1,4)]
        [SerializeField] int amountOfBullets;
        [SerializeField] Bullet bulletType;

        public override void Modify(Bullet bullet)
        {
            bullet.OnDestroyEvent += CreateBullets;
        }

        private void CreateBullets()
        {
            if (amountOfBullets > 0 && amountOfBullets < 4)
            {
                for (int i = 0; i < amountOfBullets; i++)
                {
                    var bullet = Instantiate(bulletType.gameObject, transform.position, Quaternion.Euler(0,0, Random.Range(0, 180)));
                    bullet.GetComponent<Bullet>().Init(bulletType, bullet.transform.rotation.eulerAngles, this.bullet.ShooterTransform, this.bullet.Shooter);
                }
            }
        }
    }
}