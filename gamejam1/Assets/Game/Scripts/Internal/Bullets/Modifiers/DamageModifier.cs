using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class DamageModifier : BulletModifier
    {
        public int damage;

        public float damageSelfDelay;

        public bool destroyOnHitDamagable = true;

        private float startTime = -1;

        public override void Modify(Bullet bullet)
        {
            base.Modify(bullet);

            startTime = Time.time;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (bullet.IsDestroyed)
                return;

            IDamagable damagable = collider.GetComponent<IDamagable>();

            //Check in parent as well
            //if (damagable == null)
            //    damagable = collider.GetComponentInParent<IDamagable>();

            if (damagable == null)
                return;

            //Don't damage shooter if delay isn't complete
            if (damagable.GameObject == bullet.Shooter?.GameObject && startTime + damageSelfDelay > Time.time)
                return;

            damagable.Damage(damage);

            if (destroyOnHitDamagable)
                bullet.DestroySelf();
        }


    }
}