using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    /// <summary>
    /// Flashes damage quickly - uses damage at point!
    /// </summary>
    public class DamageFlash : MonoBehaviour
    {
        public int damage;
        public float radius;

        private void Start()
        {
            DoDamage();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        private bool DoDamage()
        {
            foreach (RaycastHit2D raycast2D in Physics2D.CircleCastAll(transform.position, radius, Vector2.up, 2))
            {
                IDamagable damagable = raycast2D.collider.GetComponent<IDamagable>();

                if (damagable != null)
                    damagable.DamagePoint(damage, transform.position,radius);
            }

            return false;
        }
    }
}