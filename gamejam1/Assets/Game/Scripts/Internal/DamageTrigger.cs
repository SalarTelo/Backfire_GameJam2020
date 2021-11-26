using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    /// <summary>
    /// Simple component that damages a IDamagable when it enters the attached 2D trigger
    /// </summary>
    public class DamageTrigger : MonoBehaviour
    {
        public int damage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

            float radius = 0;

            CircleCollider2D collider = GetComponent<CircleCollider2D>();
            if (collider != null)
                radius = collider.radius;

            if (damagable != null)
                damagable.DamagePoint(damage,transform.position, radius);
        }

    }
}