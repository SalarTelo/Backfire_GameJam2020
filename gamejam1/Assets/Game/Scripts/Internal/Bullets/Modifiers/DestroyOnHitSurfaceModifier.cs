using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class DestroyOnHitSurfaceModifier : BulletModifier
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (bullet == null || bullet.IsDestroyed)
                return;

            if (collider.GetComponent<IDamagable>() != null || collider.GetComponentInParent<IDamagable>() != null)
                return;

            if (collider.GetComponent<WallModifier>() != null || collider.GetComponentInParent<WallModifier>() != null)
                return;

            if (collider.isTrigger)
                return;

            var health = collider.transform.GetComponent<HealthComponent>();
            if (health)
                health.RemoveHealth(1);
            bullet.DestroySelf();

        }

    }
}